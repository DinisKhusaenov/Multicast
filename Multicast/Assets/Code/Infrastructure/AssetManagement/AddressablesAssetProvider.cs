using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Infrastructure.AssetManagement
{
    public class AddressablesAssetProvider : ILeafAssetProvider
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
            
            var locHandle = Addressables.LoadResourceLocationsAsync(key.ToString(), typeof(T));
            IList<IResourceLocation> locs;
            try
            {
                locs = await locHandle.ToUniTask();
            }
            finally
            {
                if (locHandle.IsValid()) Addressables.Release(locHandle);
            }

            if (locs == null || locs.Count == 0)
                throw new Exception($"[Addressables] '{key.ToString()}' not found for {typeof(T).Name}");

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