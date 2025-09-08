namespace Infrastructure.SaveLoad
{
    public interface IDataProvider
    {
        void Save<T>(T data) where T : ISaveData;
        T Load<T>() where T : ISaveData, new();
    }
}