using Gameplay.Cameras;
using Gameplay.Input;
using Infrastructure.AssetManagement;
using Infrastructure.Loading.Scene;
using Infrastructure.SaveLoad;
using Infrastructure.Services.LogService;
using Infrastructure.States;
using Infrastructure.States.Factory;
using UI.HUD.Windows;
using UI.HUD.Windows.Factory;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        public override void InstallBindings()
        {
            BindSceneLoader();
            BindApplicationStateMachine();
            BindServices();
            BindCameraProvider();
            BindInputService();
            BindGameplayServices();
            BindUI();
            BindLoadingCurtain();
            BindFactory();
            BindData();
        }

        private void BindFactory()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
        }

        private void BindLoadingCurtain()
        {
            Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<ILogService>().To<LogService>().AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
        }

        private void BindApplicationStateMachine()
        {
            Container.BindInterfacesAndSelfTo<ApplicationStateMachine>().AsSingle();
        }
        
        private void BindCameraProvider()
        {
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
        }
        
        private void BindInputService()
        {
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
        }
        
        private void BindGameplayServices()
        {
            Container.Bind<ILeafAssetProvider>().To<AddressablesAssetProvider>().AsSingle();
            Container.Bind<ILeafAssetProvider>().To<ResourcesAssetProvider>().AsSingle();

            Container.Bind<IAssetProvider>().To<CompositeAssetProvider>().AsSingle();
        }

        private void BindData()
        {
            Container.BindInterfacesAndSelfTo<PersistentData>().AsSingle();
            Container.BindInterfacesAndSelfTo<DataLocalProvider>().AsSingle();
        }
    }
}