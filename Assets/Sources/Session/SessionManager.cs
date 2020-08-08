namespace Klyukay.ZigZag.Session
{
    
    public class SessionManager : StateManager<SessionManagerState>
    {
        
        internal SessionManager(IDefaultsContainer defaultsContainer, ILogProvider logProvider) 
            : base(defaultsContainer, logProvider)
        {
        }

        public float TotalDistance
        {
            get => State.TotalDistance;
            private set => State.TotalDistance = value;
        }

        public int TotalCrystals
        {
            get => State.TotalCrystals;
            private set => State.TotalCrystals = value;
        }
        
        /// <summary>
        /// Расчитывает расстояние, которое должно быть пройдено за промежуток времени dt. 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>Расстояние, пройденное за dt</returns>
        internal float CalculateDistance(float dt)
        {
            //TODO: Speed
            return dt;
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