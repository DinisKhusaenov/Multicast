using System;
using System.Linq;
using Code.Gameplay.SceneBuilder;
using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using Gameplay.Clusters.Placer;
using Gameplay.Levels.LevelCompletion;
using Infrastructure.AssetManagement;
using Infrastructure.Loading.Level;
using Infrastructure.SaveLoad;
using Infrastructure.StateMachine;
using Infrastructure.States.States;
using UI.HUD.Service;
using UI.HUD.Windows;
using UnityEngine;

namespace Gameplay.Levels
{
    public class LevelSessionService : ILevelSessionService, IDisposable
    {
        private readonly IHUDService _hudService;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IAssetProvider _assetProvider;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPersistentData _persistentData;
        private readonly IDataProvider _dataProvider;
        private readonly ILevelCompletionService _levelCompletionService;
        private readonly ISceneBuilder _sceneBuilder;
        private readonly ILevelDataLoader _levelDataLoader;

        private IClusterPlacer _clusterPlacer;
        private IClustersInitialContainer _clustersInitialContainer;
        private Transform _moveParent;
        private LevelsData _levelsData;
        private Level _currentLevel;
        private Transform _wordsParent;

        public LevelSessionService(
            IHUDService hudService,
            ILoadingCurtain loadingCurtain,
            IAssetProvider assetProvider,
            IStateSwitcher stateSwitcher,
            IPersistentData persistentData,
            IDataProvider dataProvider, 
            ISceneBuilder sceneBuilder,
            ILevelDataLoader levelDataLoader)
        {
            _hudService = hudService;
            _loadingCurtain = loadingCurtain;
            _assetProvider = assetProvider;
            _stateSwitcher = stateSwitcher;
            _persistentData = persistentData;
            _dataProvider = dataProvider;
            _sceneBuilder = sceneBuilder;
            _levelDataLoader = levelDataLoader;
        }

        public void SetUp(IClustersInitialContainer clustersInitialContainer, Transform wordsParent, Transform moveParent)
        {
            _wordsParent = wordsParent;
            _moveParent = moveParent;
            _clustersInitialContainer = clustersInitialContainer;
            
            _sceneBuilder.Initialize(_wordsParent, _clustersInitialContainer);
            _hudService.OnQuitClicked += GoToMenu;
        }

        public async UniTask Run()
        {
            _loadingCurtain.Show();
            
            await LoadLevels();
            _sceneBuilder.Build(_currentLevel);
            _clusterPlacer = new ClusterPlacer(_moveParent, _clustersInitialContainer, _sceneBuilder.Containers, _sceneBuilder.Clusters);
            _levelCompletionService.Initialize(_sceneBuilder.Containers, _currentLevel);
            _hudService.InitializeByLevel(_sceneBuilder.Containers);
            _clusterPlacer.OnClusterPlaced += CheckLevelOnComplete;
            
            _loadingCurtain.Hide();
        }
        
        public void PrepareNextLevel()
        {
            CleanUp();
            UpdateLevelData();
        }

        public void CleanUp()
        {
            _clusterPlacer.OnClusterPlaced -= CheckLevelOnComplete;
            _clusterPlacer.Dispose();
            _sceneBuilder.CleanUp();
            _clusterPlacer = null;
        }
        
        public void Dispose()
        {
            _hudService.OnQuitClicked -= GoToMenu;
        }

        private async UniTask LoadLevels()
        {
            _levelsData ??= await _levelDataLoader.LoadDataAsync();
            UpdateLevelData();
        }

        private void UpdateLevelData()
        {
            _currentLevel = _levelsData.Levels.FirstOrDefault(x => x.CurrentLevel == _persistentData.GameData.Level);
            if (_currentLevel == null)
                throw new Exception($"Level {_persistentData.GameData.Level} does not exist");
        }

        private void CheckLevelOnComplete()
        {
            if (_levelCompletionService.IsLevelCompleted())
            {
                if (_persistentData.GameData.Level >= _levelsData.Levels.Count)
                    _persistentData.GameData.Level = 0;
                
                _dataProvider.Save(_persistentData.GameData);
                _hudService.ShowGameOverView().Forget();
            }
        }

        private void GoToMenu()
        {
            _assetProvider.ReleaseAll();
            _stateSwitcher.SwitchState<MenuState>();
        }
    }
}