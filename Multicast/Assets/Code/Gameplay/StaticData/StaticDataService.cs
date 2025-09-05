using Cysharp.Threading.Tasks;
using Gameplay.Clusters.Config;
using Gameplay.Levels.Configs;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;
        private const string LevelConfigPath = "Configs/Level/LevelConfig";
        private const string ClustersConfigPath = "Configs/Cluster/ClustersConfig";
        
        public LevelConfig LevelConfig { get; private set; }
        public ClustersConfig ClustersConfig { get; private set; }

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            
            LoadLevelConfig();
            LoadClustersConfig().Forget();
        }

        private void LoadLevelConfig()
        {
            LevelConfig = Resources.Load<LevelConfig>(LevelConfigPath);
        }
        
        private async UniTask LoadClustersConfig()
        {
            ClustersConfig = await _assetProvider.Load<ClustersConfig>(ClustersConfigPath);
        }
    }
}