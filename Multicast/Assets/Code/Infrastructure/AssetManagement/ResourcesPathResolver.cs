namespace Infrastructure.AssetManagement
{
    public static class ResourcesPathResolver
    {
        public static string Resolve(AssetPathType key) => key switch
        {
            AssetPathType.LevelConfig => "Configs/Level/LevelConfig",
            AssetPathType.LevelCompletionConfig => "Configs/Level/LevelCompletionConfig",
            _ => null
        };
    }
}