using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.AssetManagement
{
    public class ResourcesAssetProvider : IAssetProvider
    {
        private readonly Dictionary<AssetPathType, Object> _cache = new();

        public async UniTask<T> Load<T>(AssetPathType key) where T : Object
        {
            if (key == AssetPathType.Unknown)
                throw new ArgumentException($"Path type {key} is invalid");

            if (_cache.TryGetValue(key, out var cached))
                return (T)cached;

            var path = ResourcesPathResolver.Resolve(key);
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException($"No Resources path mapped for {key}");

            var request = Resources.LoadAsync<T>(path);
            var asset = await request.ToUniTask();

            if (asset == null)
                throw new Exception($"Resources.LoadAsync<{typeof(T).Name}> failed by path '{path}'");

            _cache[key] = asset;
            return (T)asset;
        }

        public void Release(AssetPathType key)
        {
            if (key == AssetPathType.Unknown)
                throw new ArgumentException($"Path type {key} is invalid");

            if (_cache.TryGetValue(key, out var obj))
            {
                if (!(obj is GameObject) && !(obj is Component))
                {
                    Resources.UnloadAsset(obj);
                }

                _cache.Remove(key);
            }
        }

        public void ReleaseAll()
        {
            foreach (var obj in _cache.Values)
            {
                if (!(obj is GameObject) && !(obj is Component))
                    Resources.UnloadAsset(obj);
            }
            _cache.Clear();
        }
    }
}