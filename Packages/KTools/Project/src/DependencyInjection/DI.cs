using System;
using System.Collections.Generic;

namespace Klyukay.KTools.DependencyInjection
{
    
    public static class DI
    {
    
        private static readonly Dictionary<int, Container> ContainersById = new Dictionary<int, Container>();
        
        public static IResolver GetResolver(int id)
        {
            if (!ContainersById.TryGetValue(id, out var resolver))
            {
                throw new KeyNotFoundException($"The given key {id} was not present in the dictionary.");
            }

            return resolver;
        }

        public static Container CreateContainer(int containerId, int? parentId)
        {
            Container parent = null;
            if (parentId != null)
            {
                ContainersById.TryGetValue((int)parentId, out parent);
            }
            
            var container = new Container(containerId, parent);
            RegisterContainer(container);
            return container;
        }

        internal static void UnregisterContainer(int id)
        {
            if (!ContainersById.ContainsKey(id))
            {
                throw new ContainerNotRegisteredException($"Can't unregister container. {nameof(id)}: {id}");
            }
            ContainersById.Remove(id);
        }
        
        private static void RegisterContainer(Container container)
        {
            if (container == null) throw new ArgumentNullException($"{nameof(container)} can't be a NULL");

            var id = container.Id; 
            if (ContainersById.ContainsKey(id))
            {
                throw new ContainerAlreadyRegisteredException($"Can't register container twice. {nameof(id)}: {id}.");
            }
            ContainersById[id] = container;
        }
        
    }
    
}