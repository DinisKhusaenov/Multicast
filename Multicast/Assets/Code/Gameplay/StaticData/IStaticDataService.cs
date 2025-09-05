using Gameplay.Clusters.Config;
using Gameplay.Levels.Configs;

namespace Gameplay.StaticData
{
    public interface IStaticDataService
    {
        LevelConfig LevelConfig { get; }
        ClustersConfig ClustersConfig { get; }
    }
}