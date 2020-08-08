namespace Klyukay.ZigZag
{
    public interface IDefaultsContainer
    {

        TDefaults GetDefaults<TDefaults>() where TDefaults : class, IDefaults;

    }
}