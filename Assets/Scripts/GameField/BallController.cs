using DG.Tweening;
using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Session;
using Klyukay.ZigZag.Unity.Utils;
using UnityEngine;

using static Klyukay.ZigZag.Unity.Utils.DoTweenUtils;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    public class BallController : MonoBehaviour
    {

        private GameSessionProcessor _gameSessionProcessor;
        private SessionManager _sessionManager;
        
        private MoveDirection _currentDirection;

        private Tween _rotateTween;
        
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            var resolver = DI.GetResolver((int)GameScenes.GameScene);
            _gameSessionProcessor = resolver.Resolve<GameSessionProcessor>();
            _sessionManager = resolver.Resolve<SessionManager>();

            _sessionManager.DirectionChanged += OnDirectionChanged;
        }

        public Tween Show()
        {
            SafeKillTween(ref _rotateTween);

            return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    transform.localPosition = new Vector3(0, 10, 0);
                    gameObject.SetActive(true);
                })
                .Append(transform.DOLocalMove(Vector3.zero, 1.2f)
                    .SetEase(Ease.OutBounce));
        }

        public Tween Hide()
        {
            const float dropTime = 0.6f;

            var distance = _sessionManager.MoveSpeed * dropTime;
            var direction = _currentDirection.GetBallMoveDirection() * distance;

            return transform.DOLocalMove(new Vector3(direction.x, -10, direction.y), dropTime)
                .SetEase(Ease.InQuad);
        }

        private void OnDirectionChanged(MoveDirection direction)
        {
            if (!_sessionManager.IsSessionStarted) return;
            
            _currentDirection = direction;
            
            SafeKillTween(ref _rotateTween);
            
            if (direction == MoveDirection.NoDirection) return;

            var moveVector = direction.GetBallRotateVector();
            _rotateTween = transform.DORotate(moveVector, 1f / _sessionManager.MoveSpeed, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
        
    }
    
}