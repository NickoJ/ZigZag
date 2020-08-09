using System;
using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Session;
using TMPro;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameMenu
{
    
    [RequireComponent(typeof(TMP_Text))]
    public class InGameCrystalsTextController : MonoBehaviour
    {
     
        [SerializeField] private string templateText = "Crystals: {0:0000}";
        
        private SessionManager _sessionManager;
        
        private TMP_Text _text;
        
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            var resolver = DI.GetResolver((int) GameScenes.GameScene);

            _sessionManager = resolver.Resolve<SessionManager>();
            _sessionManager.CrystalsCountChanged += OnCrystalCountChanged;
            
            OnCrystalCountChanged(_sessionManager.TotalCrystals);            
        }

        private void OnDestroy()
        {
            _sessionManager.CrystalsCountChanged -= OnCrystalCountChanged;
        }

        private void OnCrystalCountChanged(int crystalCount) => _text.text = string.Format(templateText, crystalCount);
        
    }
    
}