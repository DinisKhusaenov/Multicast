using System.Collections.Generic;
using Gameplay.Clusters;

namespace Gameplay.Levels
{
    public interface ILevelCleanUpService
    {
        void Initialize(List<IClusterContainer> containers, List<ICluster> clusters);
        void CleanUp();
    }
}