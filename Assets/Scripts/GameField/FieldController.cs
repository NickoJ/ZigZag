using System;
using System.Collections.Generic;
using DG.Tweening;
using Klyukay.KTools;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    public class FieldController : MonoBehaviour
    {

        [SerializeField] private TileControllerPool tilesPool = default;

        private readonly List<Chunk> _chunks = new List<Chunk>();

        private ObjectPool<Chunk> _chunkPool;

        public CrystalSettings CrystalSettings { get; set; }

        public event Action<Vector2> ShowCrystalRequested;
        
        private void Awake()
        {
            _chunkPool = new ObjectPool<Chunk>(CreateChunkInstance, 0);
        }
        
        public void Move(Vector2 direction)
        {
            for (int i = 0; i < _chunks.Count; ++i)
            {
                _chunks[i].Move(direction);
            }
        }

        public bool CheckIsBallInBoundaries()
        {
            for (int i = 0; i < _chunks.Count; ++i)
            {
                if (_chunks[i].IsBallInBoundaries) return true;
            }

            return false;
        }

        public Tween ShowNewChunk(in ChunkData chunkData)
        {
            Vector2 min = chunkData.Min;
            Vector2 max = chunkData.Max;

            if (_chunks.Count > 0)
            {
                var prevChunk = _chunks[_chunks.Count - 1];
                min -= prevChunk.ChunkData.Max;
                min += prevChunk.CurrentMax;
                
                max -= prevChunk.ChunkData.Max;
                max += prevChunk.CurrentMax;
            }
            
            var chunk = GetChunkFromPool();
            _chunks.Add(chunk);

            chunk.Setup(chunkData, min, max);
            return chunk.Show().SetDelay(0.3f);
        }

        public void HideOldChunk()
        {
            var chunk = _chunks[0];

            chunk.Hide();
        }

        public Tween HideAllChunks()
        {
            var s = DOTween.Sequence();
            foreach (var chunk in _chunks)
            {
                if (chunk.IsHided) continue;
                
                s.Join(chunk.Hide()
                    .SetDelay(0.05f));
            }
            _chunks.Clear();

            return s;
        }

        private Chunk GetChunkFromPool()
        {
            var chunk = _chunkPool.Get();
            chunk.Hided += ReturnChunkToPool;
            chunk.CrystalPositionDetected += ShowCrystalRequest;
            
            return chunk;
        }

        private void ReturnChunkToPool(Chunk chunk)
        {
            _chunks.Remove(chunk);
            _chunkPool.Return(chunk);
        }

        private void ShowCrystalRequest(Vector2 position) => ShowCrystalRequested?.Invoke(position);

        private Chunk CreateChunkInstance() => new Chunk(tilesPool, CrystalSettings);

        public bool IsNeedToGenerateNewChunk() => _chunks[_chunks.Count - 1].IsToCloseToBall;

        public bool IsNeedToHideOldChunk() => !_chunks[0].IsHided && _chunks[0].IsTooBehindFromBall;
        
    }
    
}