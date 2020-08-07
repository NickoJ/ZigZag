using System;
using System.Collections.Generic;
using Klyukay.KTools.DependencyInjection.Providers;

namespace Klyukay.KTools.DependencyInjection
{
    
    public class Container : IResolver
    {

        public readonly int Id;
        
        private readonly Container _parent;
        private readonly Dictionary<string, IProvider> _providersByTypeNames = new Dictionary<string, IProvider>();

        internal Container(int id, Container parent = null)
        {
            Id = id;
            _parent = parent;
        }
        
        public void AddProvider<T>(T instance) => AddProvider<T>(new InstanceProvider<T>(instance));

        public void AddProvider<T>(IDIFabric<T> fabric) => AddProvider<T>(new FabricProvider<T>(fabric));

        public void AddProvider<T>(Func<T> func) => AddProvider<T>(new FuncProvider<T>(func));

        private void AddProvider<T>(IProvider<T> provider)
        {
            var typeName = TypeTools.GetFullName<T>();
            _providersByTypeNames[typeName] = provider;
        }

        public T Resolve<T>()
        {
            var typeName = TypeTools.GetFullName<T>();

            IProvider provider = null;
            var nextContainer = this;
            
            while (provider == null && nextContainer != null)
            {
                var container = nextContainer;
                nextContainer = container._parent;

                container._providersByTypeNames.TryGetValue(typeName, out provider);
            }

            if (provider == null)
            {
                throw new NullReferenceException(typeName);
            }

            return ((IProvider<T>)provider).Resolve();
        }
        
    }
    
}