using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.AssetManagement
{
    public class AddressablesAssetProvider : IAssetProvider
    {
        private readonly Dictionary<AssetPathType, AsyncOperationHandle> _loadedAssets = new();
        
        public async UniTask<T> Load<T>(AssetPathType key) where T : UnityEngine.Object
        {
            if (key == AssetPathType.Unknown)
                throw new ArgumentException($"Path type {key} is invalid");
            
            if (_loadedAssets.TryGetValue(key, out var handle))
            {
                return (T)handle.Result;
            }

            var operationHandle = Addressables.LoadAssetAsync<T>(key.ToString());
            _loadedAssets[key] = operationHandle;
            return await operationHandle.ToUniTask();
        }
        
        public void Release(AssetPathType key)
        {
            if (key == AssetPathType.Unknown)
                throw new ArgumentException($"Path type {key} is invalid");
            
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