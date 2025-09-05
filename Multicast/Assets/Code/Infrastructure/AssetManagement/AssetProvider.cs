using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new();
        
        public async UniTask<T> Load<T>(string key)
        {
            if (_loadedAssets.TryGetValue(key, out var handle))
            {
                return (T)handle.Result;
            }

            var operationHandle = Addressables.LoadAssetAsync<T>(key);
            _loadedAssets[key] = operationHandle;
            return await operationHandle.ToUniTask();
        }
        
        public void Release(string key)
        {
            if (_loadedAssets.TryGetValue(key, out var handle))
            {
                Addressables.Release(handle);
                _loadedAssets.Remove(key);
            }
        }
        
        public void ReleaseAll()
        {
            foreach (var handle in _loadedAssets.Values)
                Addressables.Release(handle);

            _loadedAssets.Clear();
        }
    }
}