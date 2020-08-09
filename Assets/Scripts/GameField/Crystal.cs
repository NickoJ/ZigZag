using DG.Tweening;
using Klyukay.KTools;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    
    public class Crystal : MonoBehaviour, IPoolComponent
    {

        private const float OldDistance = -5f;
        private const float CollectRadius = 0.3f;
        
        private Vector3? _defaultScale;

        public bool IsOldCrystal => transform.localPosition.x < OldDistance || transform.localPosition.z < OldDistance;

        public bool IsCollected
        {
            get
            {
                var p = transform.localPosition;

                return -CollectRadius < p.x && p.x < CollectRadius &&
                       -CollectRadius < p.z && p.z < CollectRadius;
            }
        }

        public void SetPosition(Vector2 position)
        {
            transform.localPosition = new Vector3(position.x, 0, position.y);
        }

        public void Show()
        {
            if (_defaultScale == null)
            {
                _defaultScale = transform.localScale;
            }
            
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            transform.DOScale(_defaultScale.Value, 0.2f);
        }

        public void Move(Vector2 direction)
        {
            var t = transform;
            
            var pos = t.localPosition;
            pos += new Vector3(direction.x, 0, direction.y);
            t.localPosition = pos;
        }

        void IPoolComponent.ResetItem()
        {
            gameObject.SetActive(false);
        }
    }
    
}