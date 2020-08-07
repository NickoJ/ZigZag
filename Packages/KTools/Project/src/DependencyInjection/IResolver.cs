namespace Klyukay.KTools.DependencyInjection
{
    
    public interface IResolver
    {

        T Resolve<T>();

    }
    
}