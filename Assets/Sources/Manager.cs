namespace Klyukay.ZigZag
{
    
    public abstract class Manager
    {

        protected readonly IDefaultsContainer DefaultsContainer;
        
        private readonly ILogProvider LogProvider;

        public Manager(IDefaultsContainer defaultsContainer, ILogProvider logProvider)
        {
            DefaultsContainer = defaultsContainer;
            LogProvider = logProvider;
        }        
        
    }
    
}