using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using Gameplay.Clusters.Factory;
using Gameplay.Clusters.Placer;
using Gameplay.StaticData;
using Infrastructure.AssetManagement;
using Infrastructure.Loading.Level;
using Infrastructure.StateMachine;
using Infrastructure.States.States;
using UI.HUD.Service;
using UI.HUD.Windows;
using UnityEngine;

namespace Gameplay.Levels
{
    public class LevelSessionService : ILevelSessionService, IDisposable
    {
        private readonly IClustersContainerFactory _clustersContainerFactory;
        private readonly IClusterFactory _clusterFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelDataLoader _levelDataLoader;
        private readonly ILevelCleanUpService _levelCleanUpService;
        private readonly IHUDService _hudService;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ILevelCompletionChecker _levelCompletionChecker;
        private readonly IAssetProvider _assetProvider;
        private readonly IStateSwitcher _stateSwitcher;

        private readonly List<IClusterContainer> _containers = new();
        private readonly List<ICluster> _clusters = new();
        
        private IClustersGenerator _clustersGenerator;
        private IClusterPlacer _clusterPlacer;
        private IClustersInitialContainer _clustersInitialContainer;
        private Transform _wordsParent;
        private Transform _moveParent;
        private LevelsData _levelsData;
        private int _currentLevel;

        public LevelSessionService(
            IClustersContainerFactory clustersContainerFactory, 
            IClusterFactory clusterFactory, 
            IStaticDataService staticDataService,
            ILevelDataLoader levelDataLoader,
            ILevelCleanUpService levelCleanUpService,
            IHUDService hudService,
            ILoadingCurtain loadingCurtain,
            ILevelCompletionChecker levelCompletionChecker,
            IAssetProvider assetProvider,
            IStateSwitcher stateSwitcher)
        {
            _clustersContainerFactory = clustersContainerFactory;
            _clusterFactory = clusterFactory;
            _staticDataService = staticDataService;
            _levelDataLoader = levelDataLoader;
            _levelCleanUpService = levelCleanUpService;
            _hudService = hudService;
            _loadingCurtain = loadingCurtain;
            _levelCompletionChecker = levelCompletionChecker;
            _assetProvider = assetProvider;
            _stateSwitcher = stateSwitcher;

            _hudService.OnQuitClicked += GoToMenu;
        }

        public void SetUp(IClustersInitialContainer clustersInitialContainer, Transform wordsParent, Transform moveParent)
        {
            _moveParent = moveParent;
            _clustersInitialContainer = clustersInitialContainer;
            _wordsParent = wordsParent;
            
            _clustersGenerator = new ClustersGenerator(
                _staticDataService.LevelConfig.MinClusterLength,
                _staticDataService.LevelConfig.MaxClusterLength);
        }

        public async UniTask Run()
        {
            _loadingCurtain.Show();

            await LoadLevels();
            await CreateContainers();
            CreateClusters();

            _clusterPlacer = new ClusterPlacer(
                _moveParent, 
                _clustersInitialContainer, 
                _containers,
                _clusters);
            
            _hudService.InitializeByLevel(_containers);
            _levelCleanUpService.Initialize(_containers, _clusters);
            _clusterPlacer.OnClusterPlaced += CheckLevelOnComplete;
            
            _loadingCurtain.Hide();
        }

        public void CleanUp()
        {
            _clusterPlacer.OnClusterPlaced -= CheckLevelOnComplete;
            _levelCleanUpService.CleanUp();
            _clusterPlacer.Dispose();
            _clusterPlacer = null;
            _clusters.Clear();
            _containers.Clear();
        }
        
        public void Dispose()
        {
            _hudService.OnQuitClicked -= GoToMenu;
        }

        public void PrepareNextLevel()
        {
            CleanUp();
            _currentLevel++;

            if (_currentLevel >= _levelsData.Levels.Count)
                _currentLevel = 0;
        }

        private async UniTask LoadLevels()
        {
            _levelsData ??= await _levelDataLoader.LoadDataAsync();
        }

        private async UniTask CreateContainers()
        {
            for (int i = 0; i < GetWordsCount(); i++)
            {
                var container = await _clustersContainerFactory.CreateClustersContainer(_wordsParent);
                _containers.Add(container);
            }
        }

        private void CreateClusters()
        {
            List<string> clusterTexts = _clustersGenerator.GetClusterBy(_levelsData.Levels[_currentLevel]);
            foreach (string clusterText in clusterTexts)
            {
                ICluster cluster = _clusterFactory.CreateCluster(_clustersInitialContainer.Container, clusterText);
                _clusters.Add(cluster);
            }
        }

        private void CheckLevelOnComplete()
        {
            if (_levelCompletionChecker.IsCompleted(_containers, _levelsData.Levels[_currentLevel]))
            {
                _hudService.ShowGameOverView().Forget();
            }
        }

        private int GetWordsCount()
        {
            return Math.Min(
                _levelsData.Levels[_currentLevel].Words.Count,
                _staticDataService.LevelConfig.WordsCount);
        }

        private void GoToMenu()
        {
            _assetProvider.ReleaseAll();
            _stateSwitcher.SwitchState<MenuState>();
        }
    }
}