using System.Collections.Generic;
using UnityEngine;

namespace Klyukay.KTools
{
    
    public abstract class MonoBehaviourPool<TBehaviour> : MonoBehaviour where TBehaviour : MonoBehaviour
    {

        [SerializeField] private int baseCacheCount = 0;
        [SerializeField] private TBehaviour prefab = default;

        [SerializeField] private List<TBehaviour> freeBehaviours = default;
        
        private void Awake()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                return;
            }
            
            PrepareCache();
        }
        
        public TBehaviour Get()
        {
            TBehaviour item = null;
            
            if (freeBehaviours.Count > 0)
            {
                item = freeBehaviours[freeBehaviours.Count - 1];
                freeBehaviours.RemoveAt(freeBehaviours.Count - 1);
            }
            else
            {
                item = CreateNewInstance();
            }

            return item;
        }
        
        public void Return(TBehaviour item)
        {
            if (ReferenceEquals(item, null)) return;
            
            if (item is IPoolComponent component) component.ResetItem();
            freeBehaviours.Add(item);
        }

        private void PrepareCache()
        {
            //Валидация закешированных объектов из редактора
            for (int i = 0; i < freeBehaviours.Count; ++i)
            {
                if (!ReferenceEquals(freeBehaviours[i], null)) continue;
                
                freeBehaviours.RemoveAt(i);
            }
            
            while (freeBehaviours.Count < baseCacheCount)
            {
                var instance = CreateNewInstance();
                freeBehaviours.Add(instance);
            }
        }

        private TBehaviour CreateNewInstance()
        {
            var instance = Instantiate(prefab, transform, false);

            ResetItem(instance);
            
            return instance;
        }

        private void ResetItem(TBehaviour instance)
        {
            if (instance is IPoolComponent component) component.ResetItem();
        }
        
    }
    
}