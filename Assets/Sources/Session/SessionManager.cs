using System;

namespace Klyukay.ZigZag.Session
{
    
    public class SessionManager : StateManager<SessionManagerState>
    {

        private readonly ISessionDefaults _sessionDefaults;
        
        internal SessionManager(IDefaultsContainer defaultsContainer, ILogProvider logProvider) 
            : base(defaultsContainer, logProvider)
        {
            _sessionDefaults = DefaultsContainer.GetDefaults<ISessionDefaults>();
        }

        public bool IsSessionStarted
        {
            get => State.IsSessionStarted;
            private set
            {
                if (State.IsSessionStarted == value) return;

                State.IsSessionStarted = value;
                
                if (value) SessionStarted?.Invoke();
                else SessionFinished?.Invoke();
            }
        }
        
        public MoveDirection Direction
        {
            get => State.Direction;
            internal set
            {
                if (State.Direction == value) return;
                
                State.Direction = value;
                DirectionChanged?.Invoke(value);
            }
        }
        
        public float TotalDistance
        {
            get => State.TotalDistance;
            private set => State.TotalDistance = value;
        }

        public int TotalCrystals
        {
            get => State.TotalCrystals;
            private set
            {
                if (State.TotalCrystals == value) return;
                
                State.TotalCrystals = value;
                CrystalsCountChanged?.Invoke(value);
            }
        }

        public float MoveSpeed => _sessionDefaults.MoveSpeed;

        public event Action SessionStarted;
        public event Action SessionFinished;

        public event Action<int> CrystalsCountChanged;
        public event Action<MoveDirection> DirectionChanged;

        internal void StartSession()
        {
            if (IsSessionStarted) return;

            TotalCrystals = 0;
            TotalDistance = 0f;
            
            IsSessionStarted = true;
            Direction = MoveDirection.Right;
        }

        internal void FinishSession()
        {
            if (!IsSessionStarted) return;

            IsSessionStarted = false;
            Direction = MoveDirection.NoDirection;
        }
            
        internal void DistanceTraveled(float distance)
        {
            if (distance <= 0f) return;
            
            TotalDistance += distance;
        }

        internal void CrystalsCollected(int amount)
        {
            if (amount <= 0) return;

            TotalCrystals += amount;
        }

    }
}