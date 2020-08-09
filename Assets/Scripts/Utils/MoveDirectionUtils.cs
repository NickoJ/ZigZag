using Klyukay.ZigZag.Session;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.Utils
{
    
    public static class MoveDirectionUtils
    {

        public static Vector3 GetBallRotateVector(this MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Right: return new Vector3(0, 0, -360);
                case MoveDirection.Forward: return new Vector3(360, 0, 0);
                default: return Vector3.zero;
            }
        }

        public static Vector2 GetBallMoveDirection(this MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Right: return Vector2.right;
                case MoveDirection.Forward: return Vector2.up;
                default: return Vector2.zero;
            }
        }
        
        public static Vector2 GetBlockMoveDirection(this MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Right: return Vector2.left;
                case MoveDirection.Forward: return Vector2.down;
                default: return Vector3.zero;
            }
        }
        
    }
    
}