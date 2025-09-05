using System;

namespace Gameplay.Clusters.Placer
{
    public interface IClusterPlacer : IDisposable
    {
        event Action OnClusterPlaced;
    }
}