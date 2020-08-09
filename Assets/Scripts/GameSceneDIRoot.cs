using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Unity.GameField;
using UnityEngine;

namespace Klyukay.ZigZag.Unity
{
    public class GameSceneDIRoot : DISceneRoot
    {

        [SerializeField] private GameController gameController = default;
        [SerializeField] private DifficultySettings difficultySettings = default;
        [SerializeField] private CrystalSettings crystalSettings = default;
        
        protected override ContainerInstaller CreateInstaller() => new Installer(this);

        private class Installer : ContainerInstaller
        {
            
            public override int Id => (int)GameScenes.GameScene;
            public override int? ParentId => (int) GameScenes.CoreScene;

            public Installer(GameSceneDIRoot root)
            {
                Install(container =>
                {
                    container.AddProvider<IGameController>(root.gameController);
                    container.AddProvider(root.difficultySettings);
                    container.AddProvider(root.crystalSettings);
                });
            }
            
        }
        
    }
}