using Klyukay.KTools;
using Klyukay.KTools.DependencyInjection;

namespace Klyukay.ZigZag.Unity
{
    
    internal class GameCoreInstaller : ContainerInstaller
    {
        
        public GameCoreInstaller(CoreLogic coreLogic, EventBus eventBus)
        {
            Install(container =>
            {
                container.AddProvider<IEventBus>(eventBus);
            });
        }

        public override int Id => (int)GameScenes.CoreScene;
        public override int? ParentId => default;
        
    }
    
}