using System;
using UnityEngine;

namespace Gameplay.Clusters.Config
{
    [CreateAssetMenu(menuName = "Configs/ClustersConfig", fileName = "ClustersConfig")]
    public class ClustersConfig : ScriptableObject
    {
        [SerializeField] private Cluster[] _clustersPrefab;

        public ICluster GetPrefabByLength(int length)
        {
            foreach (Cluster cluster in _clustersPrefab)
            {
                if (cluster.ClusterLength == length)
                    return cluster;
            }

            throw new ArgumentException($"Cluster by length {length} does not exist");
        }
    }
}