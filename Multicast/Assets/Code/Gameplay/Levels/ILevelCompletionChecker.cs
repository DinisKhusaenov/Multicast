using System.Collections.Generic;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;

namespace Gameplay.Levels
{
    public interface ILevelCompletionChecker
    {
        bool IsCompleted(IReadOnlyList<IClusterContainer> clusterContainers, Level level);
    }
}