using Code.Gameplay.SceneBuilder;
using Gameplay.Clusters.Factory;
using Gameplay.Levels;
using Gameplay.Levels.LevelCompletion;
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
            Container.Bind<ILevelCleanUpService>().To<LevelCleanUpService>().AsSingle();
            Container.Bind<ILevelCompletionService>().To<LevelCompletionService>().AsSingle();
            Container.Bind<ISceneBuilder>().To<SceneBuilder>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IClusterFactory>().To<ClusterFactory>().AsSingle();
            Container.Bind<IClustersContainerFactory>().To<ClustersContainerFactory>().AsSingle();
        }
    }
}