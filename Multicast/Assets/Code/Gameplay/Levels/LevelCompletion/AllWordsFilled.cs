using System.Collections.Generic;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using Infrastructure.SaveLoad;

namespace Gameplay.Levels.LevelCompletion
{
    public class AllWordsFilled : ILevelCompletionChecker
    {
        private readonly IReadOnlyList<IClusterContainer> _clusterContainers;
        private readonly IPersistentData _persistentData;
        private readonly Level _level;
        public LevelCompletionType Type => LevelCompletionType.AllWordsFilled;

        public AllWordsFilled(IReadOnlyList<IClusterContainer> clusterContainers, Level level, IPersistentData persistentData)
        {
            _clusterContainers = clusterContainers;
            _level = level;
            _persistentData = persistentData;
        }

        public bool IsCompleted()
        {
            foreach (IClusterContainer clusterContainer in _clusterContainers)
            {
                if (!_level.Words.Contains(clusterContainer.GetWordFromClusters()))
                {
                    _persistentData.GameData.Level++;
                    return false;
                }
            }

            return true;
        }
    }
}