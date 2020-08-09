using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    [CreateAssetMenu(fileName = nameof(RandomCrystalSettings), menuName = "Crystal settings/RandomCrystalSettings")]
    public class RandomCrystalSettings : CrystalSettings
    {

        [SerializeField] private int repeatPeriod = 5;

        private int _crystalIndex;
        private int _step;

        public override bool NeedToShowCrystal()
        {
            if (_step == 0)
            {
                _crystalIndex = Random.Range(0, repeatPeriod);
            }

            var oldStep = _step;
            _step = (_step + 1) % repeatPeriod;
            
            return oldStep == _crystalIndex;
        }
        
    }
    
}