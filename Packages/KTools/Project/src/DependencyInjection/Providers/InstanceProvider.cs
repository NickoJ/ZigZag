namespace Klyukay.KTools.DependencyInjection.Providers
{
    
    internal class InstanceProvider<T> : IProvider<T>
    {

        private readonly T _instance;

        internal InstanceProvider(T instance)
        {
            _instance = instance;
        }

        public T Resolve() => _instance;

    }
    
}