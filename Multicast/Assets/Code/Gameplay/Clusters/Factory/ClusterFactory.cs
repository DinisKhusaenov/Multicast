using Gameplay.Clusters.Config;
using Gameplay.StaticData;
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
            ICluster prefab = _staticDataService.GetData<ClustersConfig>().GetPrefabByLength(letters.Length);
            var cluster = _instantiator.InstantiatePrefab(prefab.gameObject, parent);
            cluster.GetComponent<ICluster>().Initialize(letters);

            return cluster.GetComponent<ICluster>();
        }
    }
}