using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Clusters;
using Gameplay.Levels;
using UI.HUD.Windows;
using UI.HUD.Windows.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD.Service
{
    public class HUDService : IHUDService
    {
        public event Action OnQuitClicked;
        
        private readonly IWindowFactory _windowFactory;
        private ILevelSessionService _levelSessionService;
        
        private Button _quitButton;
        private IReadOnlyList<IClusterContainer> _containers;
        private Canvas _canvas;
        private IGameOverView _gameOverView;

        public HUDService( 
            IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public void Initialize(ILevelSessionService levelSessionService, Canvas canvas, Button quitButton)
        {
            _canvas = canvas;
            _levelSessionService = levelSessionService;
            _quitButton = quitButton;
            
            _quitButton.onClick.AddListener(LeaveToMenu);
        }

        public void InitializeByLevel(IReadOnlyList<IClusterContainer> containers)
        {
            _containers = containers;
        }

        public void Dispose()
        {
            _quitButton.onClick.RemoveListener(LeaveToMenu);
        }
        
        public async UniTask ShowGameOverView()
        {
            _gameOverView ??= await _windowFactory.CreateGameOverView(_canvas);
            _gameOverView.Show(GetWordsFromContainer());
            _gameOverView.NextLevelClicked += StartNextLevel;
            _gameOverView.MenuClicked += LeaveToMenu;
        }

        private void RemoveCallbacks()
        {
            if (_gameOverView == null) return;
            _gameOverView.NextLevelClicked -= StartNextLevel;
            _gameOverView.MenuClicked -= LeaveToMenu;
        }

        private List<string> GetWordsFromContainer()
        {
            List<string> words = new();

            foreach (IClusterContainer container in _containers)
            {
                words.Add(container.GetWordFromClusters());
            }

            return words;
        }

        private void StartNextLevel()
        {
            _gameOverView.Hide();
            _levelSessionService.PrepareNextLevel();
            _levelSessionService.Run();

            RemoveCallbacks();
        }

        private void LeaveToMenu()
        {
            RemoveCallbacks();
            OnQuitClicked?.Invoke();
        }
        
    }
}