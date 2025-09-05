using System.Collections.Generic;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;

namespace Gameplay.Levels
{
    public class LevelCompletionChecker : ILevelCompletionChecker
    {
        public bool IsCompleted(IReadOnlyList<IClusterContainer> clusterContainers, Level level)
        {
            foreach (IClusterContainer clusterContainer in clusterContainers)
            {
                if (!level.Words.Contains(clusterContainer.GetWordFromClusters()))
                    return false;
            }

            return true;
        }
    }
}