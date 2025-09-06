using System;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.States.States;
using Zenject;

namespace UI.Menu
{
    public class GameMenuPresenter : IGameMenuPresenter, IInitializable, IDisposable
    {
        private readonly IGameMenuView _gameMenuView;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPersistentData _persistentData;

        public GameMenuPresenter(IGameMenuView gameMenuView, IStateSwitcher stateSwitcher, IPersistentData persistentData)
        {
            _gameMenuView = gameMenuView;
            _stateSwitcher = stateSwitcher;
            _persistentData = persistentData;
        }
        
        public void Initialize()
        {
            _gameMenuView.StartClicked += StartGame;
            _gameMenuView.SetLevel(_persistentData.GameData.Level + 1);
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