using System;
using Cysharp.Threading.Tasks;
using Gameplay.StaticData;
using Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Gameplay.Clusters.Factory
{
    public class ClusterFactory : IClusterFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IStaticDataService _staticDataService;

        public ClusterFactory(IInstantiator instantiator, IStaticDataService staticDataService)
        {
            _instantiator = instantiator;
            _staticDataService = staticDataService;
        }

        public ICluster CreateCluster(Transform parent, string letters)
        {
            ICluster prefab = _staticDataService.ClustersConfig.GetPrefabByLength(letters.Length);
            var cluster = _instantiator.InstantiatePrefab(prefab.gameObject, parent);
            cluster.GetComponent<ICluster>().Initialize(letters);

            return cluster.GetComponent<ICluster>();
        }
    }
}