using System;
using System.Collections.Generic;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using Gameplay.Levels.Configs;
using Gameplay.StaticData;
using Infrastructure.SaveLoad;

namespace Gameplay.Levels.LevelCompletion
{
    public class LevelCompletionService : ILevelCompletionService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentData _persistentData;

        private List<ILevelCompletionChecker> _levelCompletionCheckers = new();
        private IReadOnlyList<IClusterContainer> _containers;
        private Level _level;

        public LevelCompletionService(IStaticDataService staticDataService, IPersistentData persistentData)
        {
            _staticDataService = staticDataService;
            _persistentData = persistentData;
        }

        public void Initialize(IReadOnlyList<IClusterContainer> clustersContainers, Level level)
        {
            _level = level;
            _containers = clustersContainers;
            AddCompletionCheckers();
        }

        public bool IsLevelCompleted()
        {
            foreach (ILevelCompletionChecker checker in _levelCompletionCheckers)
            {
                if (checker.IsCompleted()) return true;
            }

            return false;
        }
        
        private void AddCompletionCheckers()
        {
            foreach (LevelCompletionType type in _staticDataService.GetData<LevelCompletionConfig>().CompletionTypes)
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