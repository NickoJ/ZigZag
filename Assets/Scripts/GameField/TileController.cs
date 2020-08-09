using DG.Tweening;
using Klyukay.KTools;
using UnityEngine;

namespace Klyukay.ZigZag.Unity.GameField
{
    public class TileController : MonoBehaviour, IPoolComponent
    {
        
        public void SetPosition(Vector2 pos)
        {
            transform.localPosition = new Vector3(pos.x, 0, pos.y);
        }

        public void Move(Vector2 direction)
        {
            var t = transform;
            
            var pos = t.localPosition;
            pos += new Vector3(direction.x, 0, direction.y);
            t.localPosition = pos;
        }

        public Tween Show()
        {
            var t = transform;
            
            var pos = t.localPosition;
            pos.y = -10;
            t.localPosition = pos;
            
            gameObject.SetActive(true);

            return t.DOLocalMoveY(0, 0.2f).SetEase(Ease.OutQuad);
        }

        public Tween Hide() => transform.DOLocalMoveY(-10, 0.2f).SetEase(Ease.InQuad);

        void IPoolComponent.ResetItem()
        {
            gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }
}