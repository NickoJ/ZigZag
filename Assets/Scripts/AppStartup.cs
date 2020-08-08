using System;
using System.Reflection;
using System.Threading;
using Klyukay.KTools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Klyukay.ZigZag.Unity
{

    public class AppStartup : MonoBehaviour
    {

        [SerializeField] private GameDefaults defaults = default;

        private GameCoreInstaller _installer;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            var eventBus = new EventBus(SynchronizationContext.Current);
            
            var coreSettings = new CoreLogicSettings.Builder
            {
                LogProvider = new UnityLogProvider(),
                DefaultsContainer = defaults,
            }.Build();
            
            var logic = new CoreLogic(coreSettings);
            
            _installer = new GameCoreInstaller(logic, eventBus);

            Resources.UnloadUnusedAssets();
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);

            SceneManager.LoadSceneAsync((int) GameScenes.GameScene);
        }

        private void OnDestroy()
        {
            _installer.Dispose();
        }
        
    }
    
}