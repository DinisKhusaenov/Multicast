using UnityEngine;

namespace Gameplay.Clusters
{
    public class ClustersInitialContainer : MonoBehaviour, IClustersInitialContainer
    {
        [field: SerializeField] public Transform Container { get; private set; }
        [field: SerializeField] public Transform Center { get; private set; }
    }
}