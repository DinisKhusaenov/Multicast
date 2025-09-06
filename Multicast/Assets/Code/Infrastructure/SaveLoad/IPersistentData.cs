namespace Infrastructure.SaveLoad
{
    public interface IPersistentData
    {
        GameData GameData { get; set; }
    }
}