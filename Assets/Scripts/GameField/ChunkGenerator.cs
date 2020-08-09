using Klyukay.ZigZag.Session;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    internal class ChunkGenerator
    {

        private readonly DifficultySettings _settings;

        private int _chunkNumber;
        
        private Vector2 _nextRightPos;
        private Vector2 _nextForwardPos;

        private MoveDirection _nextDirection;

        public ChunkGenerator(DifficultySettings settings)
        {
            _settings = settings;
        }

        public ChunkData GenerateChunkData()
        {
            ChunkData chunkData;
            
            if (_chunkNumber == 0)
            {
                chunkData = new ChunkData(new Vector2(-1, -1), new Vector2(1, 1), false);
                _nextDirection = MoveDirection.Right;
                
                _nextRightPos = new Vector2(chunkData.Max.x + 1, 
                    (chunkData.Max.y - chunkData.Min.y + 1) / 2 - _settings.LaneWidth / 2 + chunkData.Min.y);
                _nextForwardPos = new Vector2((chunkData.Max.x - chunkData.Min.x + 1) / 2 - _settings.LaneWidth / 2 + chunkData.Min.x,
                    chunkData.Max.y + 1);
            }
            else
            {
                Vector2 min;

                int heightIndex;
                int widthIndex;
                
                switch (_nextDirection)
                {
                    case MoveDirection.Right:
                        min = _nextRightPos;
                        heightIndex = 0;
                        widthIndex = 1;
                        break;
                    default:
                        min = _nextForwardPos;
                        heightIndex = 1;
                        widthIndex = 0;
                        break;
                }

                _nextDirection = _nextDirection == MoveDirection.Forward ? MoveDirection.Right : MoveDirection.Forward;

                var max = min;
                max[heightIndex] += Random.Range(_settings.LaneMinHeight, _settings.LaneMaxHeight) - 1;
                max[widthIndex] += _settings.LaneWidth - 1;
                
                chunkData = new ChunkData(min, max, true);
                
                _nextRightPos = new Vector2(max.x + 1, max.y + 1 - _settings.LaneWidth);
                _nextForwardPos = new Vector2(max.x + 1 - _settings.LaneWidth, max.y + 1);
            }

            _chunkNumber++;

            return chunkData;
        }

        public void Reset()
        {
            _chunkNumber = 0;
        }
    }
    
}