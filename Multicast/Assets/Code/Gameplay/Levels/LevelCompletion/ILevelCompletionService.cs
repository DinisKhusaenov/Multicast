using System.Collections.Generic;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;

namespace Gameplay.Levels.LevelCompletion
{
    public interface ILevelCompletionService
    {
        void Initialize(IReadOnlyList<IClusterContainer> clustersContainers, Level level);
        bool IsLevelCompleted();
    }
}