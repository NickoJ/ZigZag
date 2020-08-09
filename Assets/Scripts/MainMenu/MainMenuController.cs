using DG.Tweening;
using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Session;
using Klyukay.ZigZag.Unity.GameField;
using Klyukay.ZigZag.Unity.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static Klyukay.ZigZag.Unity.Utils.DoTweenUtils;

namespace Klyukay.ZigZag.Unity.MainMenu
{

    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public class MainMenuController : MonoBehaviour
    {

        [SerializeField] private TMP_Text title = default;
        [SerializeField] private TMP_Text tapToPlayText = default;

        [SerializeField] private TMP_Text distanceRecordText = default;
        [SerializeField] private TMP_Text crystalRecordText = default;
        
        [SerializeField] private Button button = default;

        private GameSessionProcessor _gameSessionProcessor;
        private SessionManager _sessionManager;
        
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private IGameController _gameController;
        
        private Vector2 _titleDefaultPos;

        private Tween _tapToPlayTween;
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
            
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            var resolver = DI.GetResolver((int) GameScenes.GameScene);
            _gameController = resolver.Resolve<IGameController>();
            
            _gameSessionProcessor = resolver.Resolve<GameSessionProcessor>();
            _sessionManager = resolver.Resolve<SessionManager>();
            
            _titleDefaultPos = title.rectTransform.anchoredPosition;
            button.onClick.AddListener(StartGameSession);
            
            _sessionManager.SessionStarted += Hide;
            _sessionManager.SessionFinished += Show;

            _gameController.OnFieldPrepared += UnlockGameLaunch;
            
            Show();
        }

        private void OnDestroy()
        {
            SafeKillTween(ref _tapToPlayTween);
            
            _sessionManager.SessionStarted -= Hide;
            _sessionManager.SessionFinished -= Show;
            
            _gameController.OnFieldPrepared -= UnlockGameLaunch;
        }

        private void Show()
        {
            title.rectTransform.anchoredPosition = _titleDefaultPos + new Vector2(0, 100);

            SafeKillTween(ref _tapToPlayTween);

            tapToPlayText.Alpha(0);
            distanceRecordText.Alpha(0);
            crystalRecordText.Alpha(0);

            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = false;

            DOTween.Sequence()
                .AppendInterval(0.5f)
                .Append(title.rectTransform.DOAnchorPosY(_titleDefaultPos.y, 0.2f))
                .Append(distanceRecordText.DOFade(1, 0.2f))
                .Join(crystalRecordText.DOFade(1, 0.2f).SetDelay(0.1f));
            
            _canvas.enabled = true;
        }

        private void Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(0f, 0.2f)
                .OnComplete(() => _canvas.enabled = false);
        }
        
        private void UnlockGameLaunch()
        {
            if (_canvasGroup.interactable) return;

            _canvasGroup.interactable = true;
            _tapToPlayTween = tapToPlayText.DOFade(1f, 0.5f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }
        
        private void StartGameSession()
        {
            _gameSessionProcessor.StartGameSession();
        }

    }

}