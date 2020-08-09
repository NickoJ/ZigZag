namespace Klyukay.ZigZag.Session
{
    public class SessionManagerState
    {

        public bool IsSessionStarted = false;
        
        public float TotalDistance;
        public int TotalCrystals;
        public MoveDirection Direction = MoveDirection.NoDirection;

    }
}