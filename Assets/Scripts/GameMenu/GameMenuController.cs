using DG.Tweening;
using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.ZigZag.Unity.GameMenu
{

    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public class GameMenuController : MonoBehaviour
    {

        [SerializeField] private Button switchButton = default;
        
        private SessionManager _sessionManager;
        private GameSessionProcessor _gameSessionProcessor;
        
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();

            _canvas.enabled = false;
        }

        private void Start()
        {
            var resolver = DI.GetResolver((int) GameScenes.GameScene);
            
            _sessionManager = resolver.Resolve<SessionManager>();
            _gameSessionProcessor = resolver.Resolve<GameSessionProcessor>();
            
            _sessionManager.SessionStarted += OnSessionStarted;
            _sessionManager.SessionFinished += OnSessionFinished;
            
            switchButton.onClick.AddListener(SwitchDirection);
        }

        private void OnDestroy()
        {
            _sessionManager.SessionStarted -= OnSessionStarted;
            _sessionManager.SessionFinished -= OnSessionFinished;
        }

        private void SwitchDirection() => _gameSessionProcessor.SwitchDirection();

        private void OnSessionStarted()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1f, 0.2f);
            _canvas.enabled = true;
        }

        private void OnSessionFinished()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(0f, 0.2f)
                .OnComplete(() => _canvas.enabled = false);
        }
    }

}