using Gameplay.Cameras;
using Infrastructure.Loading.Scene;
using UI.HUD.Windows;
using UnityEngine;

namespace Infrastructure.States.States
{
    public class GameState : IState
    {
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ICameraProvider _cameraProvider;
        private readonly ILoadingCurtain _loadingCurtain;

        public GameState(
            ApplicationStateMachine applicationStateMachine, 
            ISceneLoadService sceneLoadService,
            ICameraProvider cameraProvider,
            ILoadingCurtain loadingCurtain)
        {
            _applicationStateMachine = applicationStateMachine;
            _sceneLoadService = sceneLoadService;
            _cameraProvider = cameraProvider;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoadService.LoadScene(SceneNames.Game, OnLoaded);
        }

        private void OnLoaded()
        {
            _cameraProvider.SetMainCamera(Camera.main);
        }

        public void Exit()
        {
        }
    }
}
