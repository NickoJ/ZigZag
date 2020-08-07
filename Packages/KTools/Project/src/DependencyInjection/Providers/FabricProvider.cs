using System;

namespace Klyukay.KTools.DependencyInjection.Providers
{
    
    internal class FabricProvider<T> : IProvider<T>
    {

        private readonly IDIFabric<T> _fabric;
        
        internal FabricProvider(IDIFabric<T> fabric)
        {
            _fabric = fabric ?? throw new ArgumentNullException(nameof(fabric));
        }

        public T Resolve() => _fabric.CreateInstance();
        
    }
    
}