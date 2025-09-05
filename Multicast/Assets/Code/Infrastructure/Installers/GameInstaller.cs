using Gameplay.Clusters.Factory;
using Gameplay.Levels;
using Infrastructure.Loading.Level;
using UI.HUD.Service;
using Zenject;

namespace Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
            BindLevelServices();
            BindUIServices();
        }

        private void BindUIServices()
        {
            Container.Bind<IHUDService>().To<HUDService>().AsSingle();
        }

        private void BindLevelServices()
        {
            Container.Bind<ILevelDataLoader>().To<LevelDataLoader>().AsSingle();
            Container.Bind<ILevelSessionService>().To<LevelSessionService>().AsSingle();
            Container.Bind<ILevelCompletionChecker>().To<LevelCompletionChecker>().AsSingle();
            Container.Bind<ILevelCleanUpService>().To<LevelCleanUpService>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IClusterFactory>().To<ClusterFactory>().AsSingle();
            Container.Bind<IClustersContainerFactory>().To<ClustersContainerFactory>().AsSingle();
        }
    }
}