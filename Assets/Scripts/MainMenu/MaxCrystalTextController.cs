using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Records;
using TMPro;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.MainMenu
{
    
    [RequireComponent(typeof(TMP_Text))]
    public class MaxCrystalTextController : MonoBehaviour
    {
        
        [SerializeField] private string template = "Max crystals: {0:0000}";
        
        private TMP_Text _text;

        private RecordsManager _recordsManager;
        
        private void Start()
        {
            var resolver = DI.GetResolver((int) GameScenes.GameScene);
            _recordsManager = resolver.Resolve<RecordsManager>();
            
            _text = GetComponent<TMP_Text>();

            _recordsManager.CrystalRecordChanged += OnCrystalsCountChanged;
            OnCrystalsCountChanged(_recordsManager.CrystalRecord);
        }

        private void OnDestroy()
        {
            _recordsManager.CrystalRecordChanged -= OnCrystalsCountChanged;
            _recordsManager = null;
        }

        private void OnCrystalsCountChanged(int count)
        {
            _text.text = string.Format(template, count);
        }
        
    }
    
}