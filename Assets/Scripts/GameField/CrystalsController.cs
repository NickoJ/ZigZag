using System;
using System.Collections.Generic;
using Klyukay.KTools.DependencyInjection;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    public class CrystalsController : MonoBehaviour
    {

        [SerializeField] private CrystalPool pool = default;

        private readonly List<Crystal> _crystals = new List<Crystal>();

        private GameSessionProcessor _gameSessionProcessor;
        
        private void Start()
        {
            var resolver = DI.GetResolver((int)GameScenes.GameScene);
            _gameSessionProcessor = resolver.Resolve<GameSessionProcessor>();
        }

        public void ShowCrystal(Vector2 position)
        {
            var crystal = pool.Get();
            crystal.SetPosition(position);

            crystal.Show();
            
            _crystals.Add(crystal);
        }

        public void Move(Vector2 direction)
        {
            for (int i = 0; i < _crystals.Count; ++i)
            {
                _crystals[i].Move(direction);
            }
        }

        public void CheckForCollectedCrystals()
        {
            int collectedCount = 0;
            
            for (int i = 0; i < _crystals.Count; ++i)
            {
                if (!_crystals[i].IsCollected) continue;

                collectedCount += 1;
                pool.Return(_crystals[i]);
                _crystals.RemoveAt(i);
            }

            _gameSessionProcessor.CrystalsCollected(collectedCount);
        }

        public void HideOldCrystals()
        {
            int index;
            
            for (index = 0; index < _crystals.Count; ++index)
            {
                if (_crystals[index].IsOldCrystal)
                {
                    pool.Return(_crystals[index]);
                }
                else
                {
                    break;
                }
            }

            if (index > 0) _crystals.RemoveRange(0, index);
        }

        public void Reset()
        {
            foreach (var crystal in _crystals)
            {
                pool.Return(crystal);
            }
            _crystals.Clear();
        }
    }
    
}