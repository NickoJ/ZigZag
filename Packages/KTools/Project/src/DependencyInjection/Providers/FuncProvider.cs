using System;

namespace Klyukay.KTools.DependencyInjection.Providers
{
    
    internal class FuncProvider<T> : IProvider<T>
    {

        private readonly Func<T> _func;
        
        internal FuncProvider(Func<T> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public T Resolve() => _func();

    }
    
}