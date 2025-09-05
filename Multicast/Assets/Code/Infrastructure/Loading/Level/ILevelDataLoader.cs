using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;

namespace Infrastructure.Loading.Level
{
    public interface ILevelDataLoader
    {
        UniTask<LevelsData> LoadDataAsync();
    }
}