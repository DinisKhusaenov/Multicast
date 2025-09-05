using Cysharp.Threading.Tasks;
using Gameplay.Clusters;
using UnityEngine;

namespace Gameplay.Levels
{
    public interface ILevelSessionService
    {
        void SetUp(IClustersInitialContainer clustersInitialContainer, Transform wordsParent, Transform moveParent);
        UniTask Run();
        void CleanUp();
        void PrepareNextLevel();
    }
}