using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using Gameplay.Clusters.Factory;
using Gameplay.Levels;
using Gameplay.Levels.Configs;
using Gameplay.StaticData;
using UnityEngine;

namespace Code.Gameplay.SceneBuilder
{
    public class SceneBuilder : ISceneBuilder
    {
        private readonly IClustersContainerFactory _clustersContainerFactory;
        private readonly IClusterFactory _clusterFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelCleanUpService _levelCleanUpService;
        
        private IClustersGenerator _clustersGenerator;
        private Transform _wordsParent;
        private IClustersInitialContainer _clustersInitialContainer;

        public List<IClusterContainer> Containers { get; private set; } = new();
        public List<ICluster> Clusters { get; private set; } = new();

        public SceneBuilder(
            IClustersContainerFactory clustersContainerFactory, 
            IClusterFactory clusterFactory, 
            IStaticDataService staticDataService, 
            ILevelCleanUpService levelCleanUpService)
        {
            _clustersContainerFactory = clustersContainerFactory;
            _clusterFactory = clusterFactory;
            _staticDataService = staticDataService;
            _levelCleanUpService = levelCleanUpService;
        }

        public void Initialize(Transform wordsParent, IClustersInitialContainer clustersInitialContainer)
        {
            _clustersInitialContainer = clustersInitialContainer;
            _wordsParent = wordsParent;
            _levelCleanUpService.Initialize(Containers, Clusters);
            
            _clustersGenerator = new ClustersGenerator(
                _staticDataService.GetData<LevelConfig>().MinClusterLength,
                _staticDataService.GetData<LevelConfig>().MaxClusterLength);
        }

        public async UniTask Build(Level level)
        {
            await CreateContainers(level);
            CreateClusters(level);
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

        private void CreateClusters(Level level)
        {
            List<string> clusterTexts = _clustersGenerator.GetClusterBy(level);
            foreach (string clusterText in clusterTexts)
            {
                ICluster cluster = _clusterFactory.CreateCluster(_clustersInitialContainer.Container, clusterText);
                Clusters.Add(cluster);
            }
        }
        
        private int GetWordsCount(Level level)
        {
            return Math.Min(level.Words.Count, _staticDataService.GetData<LevelConfig>().WordsCount);
        }
    }
}