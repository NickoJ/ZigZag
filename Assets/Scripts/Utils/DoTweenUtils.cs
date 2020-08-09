using DG.Tweening;

namespace Klyukay.ZigZag.Unity.Utils
{
    
    public static class DoTweenUtils
    {

        public static void SafeKillTween(ref Tween tween)
        {
            if (tween == null) return;
            
            tween.Kill();
            tween = null;
        }
        
    }
    
}