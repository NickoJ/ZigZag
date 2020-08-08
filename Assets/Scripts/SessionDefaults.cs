using Klyukay.ZigZag.Session;
using UnityEngine;

namespace Klyukay.ZigZag.Unity
{
 
    [CreateAssetMenu]
    public class SessionDefaults : ScriptableObject, ISessionDefaults
    {

        private const float MinSpeed = 0.3f;
        private const float MaxSpeed = 2f;

        [SerializeField, Range(MinSpeed, MaxSpeed)] private float moveSpeed = 1f;

        public float MoveSpeed => moveSpeed;

    }
    
}