using System;
using System.Runtime.Serialization;

namespace Klyukay.KTools.DependencyInjection
{

    [Serializable]
    public class ContainerNotRegisteredException : Exception
    {

        public ContainerNotRegisteredException() {}

        public ContainerNotRegisteredException(string message) : base(message) {}

        public ContainerNotRegisteredException(string message, Exception inner) : base(message, inner) {}

        protected ContainerNotRegisteredException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) {}
            
    }
    
}