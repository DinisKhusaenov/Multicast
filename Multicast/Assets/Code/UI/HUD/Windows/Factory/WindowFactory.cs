using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace UI.HUD.Windows.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;

        public WindowFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public async UniTask<IGameOverView> CreateGameOverView(Canvas canvas)
        {
            GameObject prefab = await _assetProvider.Load<GameObject>(AssetPath.GameOverView);
            var view = _instantiator.InstantiatePrefab(prefab, canvas.transform);
            view.SetActive(false);
            
            return view.GetComponent<IGameOverView>();
        }
    }
}