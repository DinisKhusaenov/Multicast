using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;
using Infrastructure.AssetManagement;
using Infrastructure.Serialization;
using UnityEngine;

namespace Infrastructure.Loading.Level
{
    public class LevelDataLoader : ILevelDataLoader
    {
        private readonly IAssetProvider _assetProvider;

        public LevelDataLoader(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask<LevelsData> LoadDataAsync()
        {
            TextAsset levelsText = await _assetProvider.Load<TextAsset>(AssetPath.Levels);
            var data = JsonSerialization.FromJson<LevelsData>(levelsText.text);

            return data;
        }
    }
}