namespace Infrastructure.SaveLoad
{
    public interface IDataProvider
    {
        void Save();
        bool TryLoad();
    }
}