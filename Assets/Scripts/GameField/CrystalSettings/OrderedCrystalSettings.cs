using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    [CreateAssetMenu(fileName = nameof(OrderedCrystalSettings), menuName = "Crystal settings/OrderedCrystalSettings")]
    public class OrderedCrystalSettings : CrystalSettings
    {
     
        [SerializeField] private int repeatPeriod = 5;

        private int _crystalIndex;
        private int _step;

        public override bool NeedToShowCrystal()
        {
            var oldStep = _step;
            var oldCrystalIndex = _crystalIndex;
            
            _step = (_step + 1) % repeatPeriod;
            
            if (_step == 0)
            {
                _crystalIndex = (_crystalIndex + 1) % repeatPeriod;
            }
            
            return oldStep == oldCrystalIndex;
        }
        
    }
    
}