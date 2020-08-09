using System;

namespace Klyukay.ZigZag.Records
{
    
    public class RecordsManager : StateManager<RecordsManagerState>
    {
        
        internal RecordsManager(IDefaultsContainer defaultsContainer, ILogProvider logProvider) 
            : base(defaultsContainer, logProvider) { }

        public int CrystalRecord
        {
            get => State.CrystalRecord;
            private set
            {
                if (State.CrystalRecord == value) return;
                
                State.CrystalRecord = value;
                CrystalRecordChanged?.Invoke(value);
            }
        }

        public int DistanceRecord
        {
            get => State.DistanceRecord;
            private set
            {
                if (State.DistanceRecord == value) return;
                
                State.DistanceRecord = value;
                DistanceRecordChanged?.Invoke(value);
            }
        }

        public event Action<int> CrystalRecordChanged;

        public event Action<float> DistanceRecordChanged;

        internal void OnLevelFinished(int crystalCount, int distance)
        {
            CrystalRecord = Math.Max(CrystalRecord, crystalCount);
            DistanceRecord = Math.Max(DistanceRecord, distance);
        }
        
    }
}