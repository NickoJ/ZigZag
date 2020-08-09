using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    public readonly struct ChunkData
    {

        public readonly Vector2 Min;
        public readonly Vector2 Max;
        public readonly bool CanSetCrystals;

        public ChunkData(Vector2 min, Vector2 max, bool canSetCrystals)
        {
            Min = min;
            Max = max;
            CanSetCrystals = canSetCrystals;
        }
        
    }
    
}