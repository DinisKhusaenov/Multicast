using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Clusters.Config;
using Gameplay.Levels.Configs;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelConfigPath = "Configs/Level/LevelConfig";
        private const string LevelCompletionConfigPath = "Configs/Level/LevelCompletionConfig";

        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<Type, IStaticData> _datas = new();
        
        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            
            LoadLevelConfig();
            LoadLevelCompletionConfig();
            LoadClustersConfig().Forget();
        }

        public T GetData<T>() where T : IStaticData
        {
            if (_datas.TryGetValue(typeof(T), out var data))
                return (T)data;

            throw new Exception($"Data with type {typeof(T)} does not exist");
        }

        private void LoadLevelConfig()
        {
            var config = Resources.Load<LevelConfig>(LevelConfigPath);
            _datas.Add(config.GetType(), config);
        }
        
        private void LoadLevelCompletionConfig()
        {
            var config = Resources.Load<LevelCompletionConfig>(LevelCompletionConfigPath);
            _datas.Add(config.GetType(), config);
        }
        
        private async UniTask LoadClustersConfig()
        {
            var config = await _assetProvider.Load<ClustersConfig>(AssetPathType.ClustersConfig);
            _datas.Add(config.GetType(), config);
        }
    }
}