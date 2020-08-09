using System;
using DG.Tweening;
using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Session;
using Klyukay.ZigZag.Unity.Utils;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    public class GameController : MonoBehaviour, IGameController
    {

        [SerializeField] private BallController ball = default;
        [SerializeField] private FieldController field = default;
        [SerializeField] private CrystalsController crystals = default;

        private GameSessionProcessor _gameSessionProcessor;
        private SessionManager _sessionManager;

        private ChunkGenerator _chunkGenerator;
        
        public event Action OnFieldPrepared;
        
        private void Start()
        {
            var resolver = DI.GetResolver((int) GameScenes.GameScene);

            _gameSessionProcessor = resolver.Resolve<GameSessionProcessor>();
            
            _sessionManager = resolver.Resolve<SessionManager>();
            _sessionManager.SessionStarted += OnSessionStarted;
            _sessionManager.SessionFinished += OnSessionFinished;

            var crystalSettings = resolver.Resolve<CrystalSettings>();
            field.CrystalSettings = crystalSettings;
            field.ShowCrystalRequested += OnShowCrystalRequest;

            var difficultySettings = resolver.Resolve<DifficultySettings>();
            _chunkGenerator = new ChunkGenerator(difficultySettings);
            
            PrepareBaseField();
            enabled = false;
        }

        private void OnDestroy()
        {
            _sessionManager.SessionStarted -= OnSessionStarted;
            _sessionManager.SessionFinished -= OnSessionFinished;
        }

        private void Update()
        {
            _gameSessionProcessor.Tick(Time.deltaTime);
            
            MoveField();
            
            if (!field.CheckIsBallInBoundaries())
            {
                _gameSessionProcessor.FinishGameSession();
                return;
            }

            CheckForNewChunks();
            CheckForOldChunks();
            crystals.HideOldCrystals();
            crystals.CheckForCollectedCrystals();
        }

        private void OnSessionStarted() => enabled = true;

        private void OnSessionFinished()
        {
            enabled = false;
            _chunkGenerator.Reset();
            crystals.Reset();
            RestartField();
        }

        private void OnShowCrystalRequest(Vector2 position)
        {
            crystals.ShowCrystal(position);
        }

        private void MoveField()
        {
            var distance = _sessionManager.MoveSpeed * Time.deltaTime;
            var direction = _sessionManager.Direction.GetBlockMoveDirection() * distance;

            field.Move(direction);
            crystals.Move(direction);
        }

        private void CheckForNewChunks()
        {
            if (!field.IsNeedToGenerateNewChunk()) return;
            
            var chunkData = _chunkGenerator.GenerateChunkData();
            field.ShowNewChunk(chunkData);
        }

        private void CheckForOldChunks()
        {
            if (!field.IsNeedToHideOldChunk()) return;

            field.HideOldChunk();
        }
        
        private void RestartField()
        {
            DOTween.Sequence()
                .Append(ball.Hide())
                .Append(field.HideAllChunks())
                .AppendInterval(0.2f)
                .Append(PrepareBaseField());
        }
        
        private Tween PrepareBaseField()
        {
            var s = DOTween.Sequence()
                .Append(field.ShowNewChunk(_chunkGenerator.GenerateChunkData()))
                .AppendCallback(CheckForNewChunks)
                .Append(ball.Show())
                .AppendCallback(() => OnFieldPrepared?.Invoke());

            return s;
        }

    }
}