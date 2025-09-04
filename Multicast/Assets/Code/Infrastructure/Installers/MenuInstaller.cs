using UI.Menu;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private GameMenuView _gameMenuView;
        
        public override void InstallBindings()
        {
            BindViews();
            BindPresenters();
        }

        private void BindViews()
        {
            Container.Bind<IGameMenuView>().FromInstance(_gameMenuView).AsSingle();
        }

        private void BindPresenters()
        {
            Container.BindInterfacesAndSelfTo<GameMenuPresenter>().AsSingle();
        }
    }
}