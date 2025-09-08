namespace Gameplay.StaticData
{
    public interface IStaticDataService
    {
        T GetData<T>() where T : IStaticData;
    }
}