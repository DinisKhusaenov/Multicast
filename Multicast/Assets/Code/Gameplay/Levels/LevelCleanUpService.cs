using System.Collections.Generic;
using Gameplay.Clusters;
using UnityEngine;

namespace Gameplay.Levels
{
    public class LevelCleanUpService : ILevelCleanUpService
    {
        private List<IClusterContainer> _containers;
        private List<ICluster> _clusters;

        public void Initialize(List<IClusterContainer> containers, List<ICluster> clusters)
        {
            _clusters = clusters;
            _containers = containers;
        }
        
        public void CleanUp()
        {
            for (int i = _clusters.Count - 1; i >= 0; i--)
            {
                Object.Destroy(_clusters[i].gameObject);
            }
            
            for (int i = _containers.Count - 1; i >= 0; i--)
            {
                Object.Destroy(_containers[i].gameObject);
            }

            _clusters = null;
            _containers = null;
        }
    }
}