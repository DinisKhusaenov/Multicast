using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public async UniTask<T> Load<T>(string key)
        {
            return await Addressables.LoadAssetAsync<T>(key).ToUniTask();
        }
    }
}