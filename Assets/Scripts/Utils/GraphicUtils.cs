using UnityEngine.UI;

namespace Klyukay.ZigZag.Unity.Utils
{
    
    public static class GraphicUtils
    {

        public static void Alpha(this Graphic graphic, float value)
        {
            var clr = graphic.color;
            clr.a = value;
            graphic.color = clr;
        }
        
    }
    
}