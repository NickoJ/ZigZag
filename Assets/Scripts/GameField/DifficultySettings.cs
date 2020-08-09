using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    [CreateAssetMenu]
    public class DifficultySettings : ScriptableObject
    {

        [SerializeField] private float laneWidth = 1;
        [SerializeField] private int laneMinHeight = 1;
        [SerializeField] private int laneMaxHeight = 5;

        public float LaneWidth => laneWidth;

        public int LaneMinHeight => laneMinHeight;
        public int LaneMaxHeight => laneMaxHeight;
        
    }
    
}