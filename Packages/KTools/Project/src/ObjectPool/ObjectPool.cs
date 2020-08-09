using System;
using System.Collections.Generic;

namespace Klyukay.KTools
{
    
    public class ObjectPool<TItem> where TItem : class
    {

        private readonly Func<TItem> _creator;
        private readonly List<TItem> _freeItems = new List<TItem>();
        
        public ObjectPool(Func<TItem> creator, int cacheSize)
        {
            _creator = creator ?? throw new NullReferenceException(nameof(creator));
            
            PrepareCache(cacheSize);
        }
        
        public TItem Get()
        {
            TItem item = default;
            
            if (_freeItems.Count > 0)
            {
                item = _freeItems[_freeItems.Count - 1];
                _freeItems.RemoveAt(_freeItems.Count - 1);
            }
            else
            {
                item = _creator();
                ResetInstance(item);
            }

            return item;
        }

        public void Return(TItem item)
        {
            if (item == null) return;
            
            ResetInstance(item);
            _freeItems.Add(item);
        }

        private void PrepareCache(int cacheSize)
        {
            while (_freeItems.Count < cacheSize)
            {
                var instance = _creator();
                ResetInstance(instance);
                _freeItems.Add(instance);
            }
        }

        private void ResetInstance(TItem item)
        {
            if (item is IPoolComponent component) component.ResetItem();
        }
    }
    
}