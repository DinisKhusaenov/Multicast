using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Gameplay.Clusters.Factory
{
    public class ClustersContainerFactory : IClustersContainerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;

        public ClustersContainerFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<IClusterContainer> CreateClustersContainer(Transform parent)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetPath.ClustersContainer);
            var cluster = _instantiator.InstantiatePrefab(prefab, parent);

            return cluster.GetComponent<IClusterContainer>();
        }
    }
}