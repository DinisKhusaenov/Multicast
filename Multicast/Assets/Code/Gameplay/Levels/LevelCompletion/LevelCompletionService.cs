using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using Gameplay.Levels.Configs;
using Infrastructure.AssetManagement;
using Infrastructure.SaveLoad;

namespace Gameplay.Levels.LevelCompletion
{
    public class LevelCompletionService : ILevelCompletionService
    {
        private readonly IPersistentData _persistentData;
        private readonly IAssetProvider _assetProvider;

        private List<ILevelCompletionChecker> _levelCompletionCheckers = new();
        private IReadOnlyList<IClusterContainer> _containers;
        private Level _level;

        public LevelCompletionService(IPersistentData persistentData, IAssetProvider assetProvider)
        {
            _persistentData = persistentData;
            _assetProvider = assetProvider;
        }

        public void Initialize(IReadOnlyList<IClusterContainer> clustersContainers, Level level)
        {
            _level = level;
            _containers = clustersContainers;
            AddCompletionCheckers().Forget();
        }

        public bool IsLevelCompleted()
        {
            foreach (ILevelCompletionChecker checker in _levelCompletionCheckers)
            {
                if (checker.IsCompleted()) return true;
            }

            return false;
        }
        
        private async UniTask AddCompletionCheckers()
        {
            var config = await _assetProvider.Load<LevelCompletionConfig>(AssetPathType.LevelCompletionConfig);
            foreach (LevelCompletionType type in config.CompletionTypes)
            {
                switch (type)
                {
                    case LevelCompletionType.AllWordsFilled:
                        _levelCompletionCheckers.Add(new AllWordsFilled(_containers, _level, _persistentData));
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException($"Completion type {type} does not exist");
                }
            }
        }
    }
}