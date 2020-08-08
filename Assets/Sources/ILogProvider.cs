using System;

namespace Klyukay.ZigZag
{
    
    public interface ILogProvider
    {

        void Debug(string message);
        
        void Warning(string message);
        
        void Error(string message);
        
        void Exception(Exception e);

    }
    
}