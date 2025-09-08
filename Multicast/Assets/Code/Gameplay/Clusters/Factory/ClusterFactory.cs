using Cysharp.Threading.Tasks;
using Gameplay.Clusters.Config;
using Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Gameplay.Clusters.Factory
{
    public class ClusterFactory : IClusterFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IAssetProvider _assetProvider;

        public ClusterFactory(IInstantiator instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public async UniTask<ICluster> CreateCluster(Transform parent, string letters)
        {
            var config = await _assetProvider.Load<ClustersConfig>(AssetPathType.ClustersConfig);
            ICluster prefab = config.GetPrefabByLength(letters.Length);
            var cluster = _instantiator.InstantiatePrefab(prefab.gameObject, parent);
            cluster.GetComponent<ICluster>().Initialize(letters);

            return cluster.GetComponent<ICluster>();
        }
    }
}