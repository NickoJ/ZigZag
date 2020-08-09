using Klyukay.ZigZag.Records;
using Klyukay.ZigZag.Session;

namespace Klyukay.ZigZag
{
    
    /// <summary>
    /// Класс отвечающий за взаимодействие с менеджерами из вне и менеджеров друг с другом.
    /// </summary>
    public class GameSessionProcessor : IProcessor
    {

        private readonly SessionManager _sessionManager;
        private readonly RecordsManager _recordsManager;

        internal GameSessionProcessor(SessionManager sessionManager, RecordsManager recordsManager)
        {
            _sessionManager = sessionManager;
            _recordsManager = recordsManager;
        }
        
        public void StartGameSession()
        {
            _sessionManager.StartSession();
        }

        public void FinishGameSession()
        {
            _recordsManager.OnLevelFinished(_sessionManager.TotalCrystals, (int)_sessionManager.TotalDistance);
            _sessionManager.FinishSession();
        }

        public void SwitchDirection()
        {
            if (!_sessionManager.IsSessionStarted) return;

            var oldDir = _sessionManager.Direction; 
            if (oldDir == MoveDirection.NoDirection) return;

            _sessionManager.Direction = oldDir == MoveDirection.Forward ? MoveDirection.Right : MoveDirection.Forward;
        }

        public void Tick(float dt)
        {
            _sessionManager.DistanceTraveled(_sessionManager.MoveSpeed * dt);
        }

        public void CrystalsCollected(int count)
        {
            _sessionManager.CrystalsCollected(count);
        }
        
    }
    
}