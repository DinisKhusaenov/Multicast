using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Clusters.Factory
{
    public interface IClustersContainerFactory
    {
        UniTask<IClusterContainer> CreateClustersContainer(Transform parent);
    }
}