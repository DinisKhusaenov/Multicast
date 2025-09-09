using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using Gameplay.Clusters.Factory;
using Gameplay.Levels;
using Gameplay.Levels.Configs;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Code.Gameplay.SceneBuilder
{
    public class SceneBuilder : ISceneBuilder
    {
        private readonly IClustersContainerFactory _clustersContainerFactory;
        private readonly IClusterFactory _clusterFactory;
        private readonly ILevelCleanUpService _levelCleanUpService;
        private readonly IAssetProvider _assetProvider;

        private IClustersGenerator _clustersGenerator;
        private Transform _wordsParent;
        private IClustersInitialContainer _clustersInitialContainer;
        private LevelConfig _levelConfig;

        public List<IClusterContainer> Containers { get; private set; } = new();
        public List<ICluster> Clusters { get; private set; } = new();

        public SceneBuilder(
            IClustersContainerFactory clustersContainerFactory, 
            IClusterFactory clusterFactory, 
            ILevelCleanUpService levelCleanUpService,
            IAssetProvider assetProvider)
        {
            _clustersContainerFactory = clustersContainerFactory;
            _clusterFactory = clusterFactory;
            _levelCleanUpService = levelCleanUpService;
            _assetProvider = assetProvider;
        }

        public async UniTask InitializeAsync(Transform wordsParent, IClustersInitialContainer clustersInitialContainer)
        {
            _clustersInitialContainer = clustersInitialContainer;
            _wordsParent = wordsParent;
            _levelCleanUpService.Initialize(Containers, Clusters);
            
            _levelConfig = await _assetProvider.Load<LevelConfig>(AssetPathType.LevelConfig);
            _clustersGenerator = new ClustersGenerator(_levelConfig.MinClusterLength, _levelConfig.MaxClusterLength);
        }

        public async UniTask Build(Level level)
        {
            await CreateContainers(level);
            await CreateClusters(level);
        }
        
        public void CleanUp()
        {
            _levelCleanUpService.CleanUp();
        }
        
        private async UniTask CreateContainers(Level level)
        {
            for (int i = 0; i < GetWordsCount(level); i++)
            {
                var container = await _clustersContainerFactory.CreateClustersContainer(_wordsParent);
                Containers.Add(container);
            }
        }

        private async UniTask CreateClusters(Level level)
        {
            List<string> clusterTexts = _clustersGenerator.GetClusterBy(level);
            foreach (string clusterText in clusterTexts)
            {
                ICluster cluster = await _clusterFactory.CreateCluster(_clustersInitialContainer.Container, clusterText);
                Clusters.Add(cluster);
            }
        }
        
        private int GetWordsCount(Level level)
        {
            return Math.Min(level.Words.Count, _levelConfig.WordsCount);
        }
    }
}