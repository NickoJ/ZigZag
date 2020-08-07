using System;
using UnityEngine;

namespace Klyukay.KTools.DependencyInjection
{
    
    public abstract class DISceneRoot : MonoBehaviour
    {

        private ContainerInstaller _installer;

        protected int Id => _installer.Id;
        
        public virtual void Awake()
        {
            _installer = CreateInstaller() ?? throw new NullReferenceException(
                             $"Installer must be created in \"{nameof(CreateInstaller)}\" method.");
        }

        public virtual void OnDestroy()
        {
            _installer.Dispose();
            _installer = null;
        }

        protected abstract ContainerInstaller CreateInstaller();

    }
    
}