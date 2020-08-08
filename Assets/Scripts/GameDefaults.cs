using System;
using System.Collections.Generic;
using Klyukay.ZigZag.Session;
using UnityEngine;

namespace Klyukay.ZigZag.Unity
{
    
    [CreateAssetMenu]
    public class GameDefaults : ScriptableObject, IDefaultsContainer
    {

        [SerializeField] private SessionDefaults sessionDefaults = default;

        private Dictionary<Type, IDefaults> _defaultsByType;
        
        public TDefaults GetDefaults<TDefaults>() where TDefaults : class, IDefaults
        {
            ValidateDictionary();
            return (TDefaults) _defaultsByType[typeof(TDefaults)];
        }

        private void ValidateDictionary()
        {
            if (_defaultsByType != null) return;
            
            _defaultsByType = new Dictionary<Type, IDefaults>
            {
                [typeof(ISessionDefaults)] = sessionDefaults
            };
        }
        
    }
    
}