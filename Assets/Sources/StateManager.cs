namespace Klyukay.ZigZag
{
    
    public abstract class StateManager<TState> : Manager 
        where TState : class, new()
    {

        protected readonly TState State;
        
        protected internal StateManager(IDefaultsContainer defaultsContainer, ILogProvider logProvider)
            : base(defaultsContainer, logProvider)
        {
            State = new TState();
        }
        
    }
    
}