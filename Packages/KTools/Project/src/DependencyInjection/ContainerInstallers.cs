using System;

namespace Klyukay.KTools.DependencyInjection
{
    
    public abstract class ContainerInstaller : IDisposable
    {

        public abstract int Id { get; }
        public abstract int? ParentId { get; }
        
        protected void Install()
        {
            var container = DI.CreateContainer(Id, ParentId);
            RegisterProviders(container);
        }
        
        public void Dispose()
        {
            DI.UnregisterContainer(Id);
        }

        protected abstract void RegisterProviders(Container container);

    }

}