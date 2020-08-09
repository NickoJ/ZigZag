using System;
using System.Collections.Generic;
using DG.Tweening;
using Klyukay.KTools;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    public class Chunk : IPoolComponent
    {

        private const float ValidMinDistance = 0.6f;
        private const float ValidMaxDistance = -0.6f;

        private const float TooCloseDistance = 4f;
        private const float TooBehindDistance = -1.5f;

        private readonly TileControllerPool _tilesPool;
        private readonly CrystalSettings _crystalSettings;
        private readonly List<TileController> _tiles = new List<TileController>();

        public Chunk(TileControllerPool tilesPool, CrystalSettings crystalSettings)
        {
            _tilesPool = tilesPool;
            _crystalSettings = crystalSettings;
        }
        
        public bool IsBallInBoundaries => CurrentMin.x < ValidMinDistance 
                                          && CurrentMin.y < ValidMinDistance 
                                          && CurrentMax.x > ValidMaxDistance 
                                          && CurrentMax.y > ValidMaxDistance;

        public bool IsToCloseToBall => CurrentMax.x < TooCloseDistance || CurrentMax.y < TooCloseDistance;

        public bool IsTooBehindFromBall => CurrentMax.x < TooBehindDistance || CurrentMax.y < TooBehindDistance;

        public ChunkData ChunkData { get; private set; }
        
        public Vector2 CurrentMin { get; private set; }
        public Vector2 CurrentMax { get; private set; }
        
        public bool IsHided { get; private set; }

        public event Action<Chunk> Hided;
        public event Action<Vector2> CrystalPositionDetected;
        
        public void Setup(in ChunkData chunkData, Vector2 min, Vector2 max)
        {
            ChunkData = chunkData;

            CurrentMin = min;
            CurrentMax = max;
            
            for (float x = min.x; x <= max.x; x += 1)
            {
                for (float y = min.y; y <= max.y; y += 1)
                {
                    var tile = _tilesPool.Get();
                    var position = new Vector2(x, y);
                    tile.SetPosition(position);

                    if (chunkData.CanSetCrystals && _crystalSettings.NeedToShowCrystal())
                    {
                        CrystalPositionDetected?.Invoke(position);
                    }
                    
                    _tiles.Add(tile);
                }
            }
        }

        public void Move(Vector2 direction)
        {
            CurrentMin += direction;
            CurrentMax += direction;

            for (int i = 0; i < _tiles.Count; ++i)
            {
                _tiles[i].Move(direction);
            }
        }

        public Tween Show()
        {
            const float delay = 0.05f;

            var sequence = DOTween.Sequence();

            foreach (var tile in _tiles)
            {
                sequence.Join(tile.Show().SetDelay(delay));
            }

            return sequence;        
        }

        public Tween Hide()
        {
            IsHided = true;

            var s = DOTween.Sequence();
            foreach (var tile in _tiles)
            {
                s.Join(tile.Hide()
                    .SetDelay(0.05f));
            }

            s.AppendCallback(() => Hided?.Invoke(this));
            return s;
        }

        void IPoolComponent.ResetItem()
        {
            Hided = null;
            CrystalPositionDetected = null;
            
            IsHided = false;
            
            foreach (var tile in _tiles)
            {
                _tilesPool.Return(tile);
            }
            
            _tiles.Clear();
        }
    }
    
}