using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Clusters.Placer
{
    public class ClusterPlacer : IClusterPlacer
    {
        public event Action OnClusterPlaced;
        
        private readonly Transform _moveParent;
        private readonly IClustersInitialContainer _clustersStartContainer;
        
        private List<IClusterContainer> _clustersContainers;
        private List<ICluster> _clusters;

        public ClusterPlacer(
            Transform moveParent, 
            IClustersInitialContainer clustersStartContainer, 
            List<IClusterContainer> clustersContainers, 
            List<ICluster> clusters)
        {
            _moveParent = moveParent;
            _clustersStartContainer = clustersStartContainer;
            _clustersContainers = clustersContainers;
            _clusters = clusters;

            AddEvents();
        }
        
        public void Dispose()
        {
            foreach (ICluster cluster in _clusters)
            {
                cluster.OnDragBegun -= OnClusterDragBegun;
                cluster.OnDragEnded -= OnClusterDragEnded;
            }
        }

        private void AddEvents()
        {
            foreach (ICluster cluster in _clusters)
            {
                cluster.OnDragBegun += OnClusterDragBegun;
                cluster.OnDragEnded += OnClusterDragEnded;
            }
        }

        private void OnClusterDragBegun(Cluster cluster)
        {
            cluster.transform.SetParent(_moveParent);
            cluster.CurrentClusterContainer?.RemoveCluster(cluster);
        }

        private void OnClusterDragEnded(Cluster cluster)
        {
            IClusterContainer clusterContainer = GetClosestClusterContainer(cluster, out float distanceToContainer);

            if (distanceToContainer <= GetDistanceToStartContainer(cluster) &&
                clusterContainer.FreeSize >= cluster.ClusterLength)
            {
                clusterContainer.AddCluster(cluster);
                OnClusterPlaced?.Invoke();
            }
            else
            {
                cluster.transform.SetParent(_clustersStartContainer.Container);
            }
        }

        private IClusterContainer GetClosestClusterContainer(Cluster cluster, out float distance)
        {
            distance = float.MaxValue;
            IClusterContainer closestClusterContainer = null;
            
            foreach (IClusterContainer container in _clustersContainers)
            {
                float distanceToContainer = Vector2.Distance(cluster.transform.position, container.gameObject.transform.position);
                
                if (distance > distanceToContainer)
                {
                    distance = distanceToContainer;
                    closestClusterContainer = container;
                }
            }

            return closestClusterContainer;
        }

        private float GetDistanceToStartContainer(Cluster cluster)
        {
            return Vector2.Distance(cluster.transform.position, _clustersStartContainer.Center.position);
        }
    }
}