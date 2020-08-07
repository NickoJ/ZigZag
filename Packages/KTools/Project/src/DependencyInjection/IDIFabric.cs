namespace Klyukay.KTools.DependencyInjection
{
    
    public interface IDIFabric<out T>
    {
        
        T CreateInstance();
        
    }
    
}