namespace Klyukay.ZigZag
{
    
    public class CoreLogicSettings
    {

        internal readonly IDefaultsContainer DefaultsContainer;
        internal readonly ILogProvider LogProvider;

        public CoreLogicSettings(IDefaultsContainer defaultsContainer, ILogProvider logProvider)
        {
            DefaultsContainer = defaultsContainer;
            LogProvider = logProvider;
        }
        
        public class Builder
        {

            public IDefaultsContainer DefaultsContainer { get; set; }
            public ILogProvider LogProvider { get; set; }
            
            public CoreLogicSettings Build() => new CoreLogicSettings(DefaultsContainer, LogProvider);

        }
        
    }
    
}