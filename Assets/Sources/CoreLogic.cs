using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Klyukay.ZigZag
{

    public class CoreLogic
    {

        private readonly Dictionary<Type, Manager> _managersByType = new Dictionary<Type, Manager>();
        
        public CoreLogic(CoreLogicSettings settings)
        {
            InitializeManagers(settings);
        }

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
        
    }

}