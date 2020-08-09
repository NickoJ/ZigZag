using System;
using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Records;
using Klyukay.ZigZag.Session;

namespace Klyukay.ZigZag.Unity
{
    
    internal class CoreInstaller : ContainerInstaller
    {

        private readonly CoreLogic _logic;
        
        public CoreInstaller(CoreLogic coreLogic)
        {
            _logic = coreLogic ?? throw new NullReferenceException(nameof(coreLogic));
            
            Install(Register);
        }

        public override int Id => (int)GameScenes.CoreScene;
        public override int? ParentId => default;

        private void Register(Container container)
        {
            container.AddProvider(_logic.GetManager<SessionManager>);
            container.AddProvider(_logic.GetManager<RecordsManager>);
            
            container.AddProvider(_logic.GetProcessor<GameSessionProcessor>);
        }
        
    }
    
}