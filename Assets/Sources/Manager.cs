using System;

namespace Klyukay.ZigZag
{
    
    public abstract class Manager
    {

        protected readonly IDefaultsContainer DefaultsContainer;
        
        private readonly ILogProvider _logProvider;

        protected internal Manager(IDefaultsContainer defaultsContainer, ILogProvider logProvider)
        {
            DefaultsContainer = defaultsContainer;
            _logProvider = logProvider;
        }

        protected void LogDebug(string message) => _logProvider?.Debug(message);
        
        protected void LogWarning(string message) => _logProvider?.Warning(message);
        
        protected void LogError(string message) => _logProvider?.Error(message);

        protected void LogException(Exception e) => _logProvider?.Exception(e);

    }
    
}