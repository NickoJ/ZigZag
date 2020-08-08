using System;

namespace Klyukay.ZigZag.Unity
{
    
    public class UnityLogProvider : ILogProvider
    {
        
        public void Debug(string message) => UnityEngine.Debug.Log(message);

        public void Warning(string message) => UnityEngine.Debug.LogWarning(message);

        public void Error(string message) => UnityEngine.Debug.LogError(message);

        public void Exception(Exception e) => UnityEngine.Debug.LogException(e);
        
    }
    
}