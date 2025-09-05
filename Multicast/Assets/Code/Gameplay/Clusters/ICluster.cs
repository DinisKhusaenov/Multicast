using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Clusters
{
    public interface ICluster: IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        event Action<Cluster> OnDragBegun;
        event Action<Cluster> OnDragEnded;
        
        GameObject gameObject { get; }
        int ClusterLength { get; }
        string Letters { get; }
        IClusterContainer CurrentClusterContainer { get; }
        
        void Initialize(string letters);
        void SetContainer(IClusterContainer clusterContainer);
    }
}