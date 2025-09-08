using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.LogService;
using Zenject;

namespace Infrastructure.AssetManagement
{
    public class CompositeAssetProvider : IAssetProvider
    {
        [Inject] private ILogService _logService;
        private List<IAssetProvider> _providers = new();

        public CompositeAssetProvider(params IAssetProvider[] providers)
        {
            _providers.AddRange(providers);
        }
        
        public async UniTask<T> Load<T>(AssetPathType key) where T : UnityEngine.Object
        {
            foreach (var provider in _providers)
            {
                try
                {
                    var result = await provider.Load<T>(key);
                    if (result != null)
                        return result;
                }
                catch
                {
                    _logService.Log($"Asset with provider type {provider} not loaded");
                }
            }

            throw new Exception($"Asset {key} not found in any provider");
        }

        public void Release(AssetPathType key)
        {
            foreach (var provider in _providers)
            {
                try
                {
                    provider.Release(key);
                }
                catch
                {
                    _logService.LogWarning($"Asset with key {key} is not released");
                }
            }
        }

        public void ReleaseAll()
        {
            foreach (var provider in _providers)
            {
                provider.ReleaseAll();
            }
        }
    }
}