using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Clusters.Factory
{
    public interface IClusterFactory
    {
        ICluster CreateCluster(Transform parent, string letters);
    }
}