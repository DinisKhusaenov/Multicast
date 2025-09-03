using Gameplay.Cameras;
using Infrastructure.AssetManagement;
using Infrastructure.Loading.Scene;
using Infrastructure.Services.LogService;
using Infrastructure.States;
using Infrastructure.States.Factory;
using UI.HUD.Windows;
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
            BindGameplayServices();
            BindLoadingCurtain();
            BindFactory();
        }

        private void BindFactory()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
        }

        private void BindLoadingCurtain()
        {
            Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
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
        
        private void BindGameplayServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }
    }
}