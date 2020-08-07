namespace Klyukay.KTools.DependencyInjection.Providers
{

    internal interface IProvider { }
    
    internal interface IProvider<T> : IProvider
    {

        T Resolve();

    }
    
}