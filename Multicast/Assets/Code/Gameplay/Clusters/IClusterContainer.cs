using UnityEngine;

namespace Gameplay.Clusters
{
    public interface IClusterContainer
    {
        GameObject gameObject { get; }
        int FreeSize { get; }
        void AddCluster(ICluster cluster);
        void RemoveCluster(ICluster cluster);
        string GetWordFromClusters();
    }
}