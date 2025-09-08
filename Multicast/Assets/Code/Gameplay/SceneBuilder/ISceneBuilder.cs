using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using UnityEngine;

namespace Code.Gameplay.SceneBuilder
{
    public interface ISceneBuilder
    {
        List<IClusterContainer> Containers { get; }
        List<ICluster> Clusters { get; }
        void Initialize(Transform wordsParent, IClustersInitialContainer clustersInitialContainer);
        UniTask Build(Level level);
        void CleanUp();
    }
}