using System;
using System.Runtime.Serialization;

namespace Klyukay.KTools.DependencyInjection
{

    [Serializable]
    public class ContainerAlreadyRegisteredException : Exception
    {
        
        public ContainerAlreadyRegisteredException() { }

        public ContainerAlreadyRegisteredException(string message) : base(message) { }

        public ContainerAlreadyRegisteredException(string message, Exception inner) : base(message, inner) { }

        protected ContainerAlreadyRegisteredException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
        
    }
    
}