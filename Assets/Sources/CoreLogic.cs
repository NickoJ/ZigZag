using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Klyukay.ZigZag.Records;
using Klyukay.ZigZag.Session;

namespace Klyukay.ZigZag
{

    public class CoreLogic
    {

        private readonly Dictionary<Type, Manager> _managersByType = new Dictionary<Type, Manager>();
        private readonly Dictionary<Type, IProcessor> _processorsByType = new Dictionary<Type, IProcessor>();
        
        public CoreLogic(CoreLogicSettings settings)
        {
            InitializeManagers(settings);
            InitializeProcessors();
        }

        public T GetManager<T>() where T : Manager => (T)_managersByType[typeof(T)];

        public T GetProcessor<T>() where T : IProcessor => (T) _processorsByType[typeof(T)];

        private void InitializeManagers(CoreLogicSettings settings)
        {
            var managerType = typeof(Manager);
            
            var managers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => t.IsSubclassOf(managerType))
                .Select(t => (Manager)Activator.CreateInstance(t,
                    BindingFlags.Instance | BindingFlags.NonPublic, null, new object[]
                    {
                        settings.DefaultsContainer, 
                        settings.LogProvider
                    }, null))
                .ToArray();

            foreach (var manager in managers)
            {
                _managersByType[manager.GetType()] = manager;
            }
        }

        private void InitializeProcessors()
        {
            _processorsByType[typeof(GameSessionProcessor)] = 
                new GameSessionProcessor(GetManager<SessionManager>(), GetManager<RecordsManager>());
        }
        
    }

}