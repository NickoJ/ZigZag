using System;

namespace Klyukay.KTools.DependencyInjection
{
    
    public abstract class ContainerInstaller : IDisposable
    {

        public abstract int Id { get; }
        public abstract int? ParentId { get; }
        
        protected void Install(Action<Container> registrar)
        {
            var container = DI.CreateContainer(Id, ParentId);
            registrar(container);
        }
        
        public void Dispose()
        {
            DI.UnregisterContainer(Id);
        }

    }

}