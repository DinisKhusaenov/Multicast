using UnityEngine;

namespace Gameplay.Clusters
{
    public interface IClustersInitialContainer
    {
        Transform Container { get; }
        Transform Center { get; }
    }
}