using System;
using Infrastructure.StateMachine;
using Infrastructure.States.States;
using Zenject;

namespace UI.Menu
{
    public class GameMenuPresenter : IGameMenuPresenter, IInitializable, IDisposable
    {
        private readonly IGameMenuView _gameMenuView;
        private readonly IStateSwitcher _stateSwitcher;

        public GameMenuPresenter(IGameMenuView gameMenuView, IStateSwitcher stateSwitcher)
        {
            _gameMenuView = gameMenuView;
            _stateSwitcher = stateSwitcher;
        }
        
        public void Initialize()
        {
            _gameMenuView.StartClicked += StartGame;
        }
        
        public void Dispose()
        {
            _gameMenuView.StartClicked -= StartGame;
        }
        
        public void StartGame()
        {
            _stateSwitcher.SwitchState<GameState>();
        }
    }
}