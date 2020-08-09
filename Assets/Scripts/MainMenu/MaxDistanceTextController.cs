using Klyukay.KTools.DependencyInjection;
using Klyukay.ZigZag.Records;
using TMPro;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.MainMenu
{

    [RequireComponent(typeof(TMP_Text))]
    public class MaxDistanceTextController : MonoBehaviour
    {

        [SerializeField] private string template = "Max distance: {0:0000}";
        
        private TMP_Text _text;

        private RecordsManager _recordsManager;
        
        private void Start()
        {
            var resolver = DI.GetResolver((int) GameScenes.GameScene);
            _recordsManager = resolver.Resolve<RecordsManager>();
            
            _text = GetComponent<TMP_Text>();

            _recordsManager.DistanceRecordChanged += OnDistanceChanged;
            OnDistanceChanged(_recordsManager.DistanceRecord);
        }

        private void OnDestroy()
        {
            _recordsManager.DistanceRecordChanged -= OnDistanceChanged;
            _recordsManager = null;
        }

        private void OnDistanceChanged(float distance)
        {
            _text.text = string.Format(template, distance);
        }
        
    }

}