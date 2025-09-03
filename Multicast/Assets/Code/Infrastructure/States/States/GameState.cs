using Gameplay.Cameras;
using Infrastructure.Loading.Scene;
using UnityEngine;

namespace Infrastructure.States.States
{
    public class GameState : IState
    {
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ICameraProvider _cameraProvider;

        public GameState(
            ApplicationStateMachine applicationStateMachine, 
            ISceneLoadService sceneLoadService,
            ICameraProvider cameraProvider)
        {
            _applicationStateMachine = applicationStateMachine;
            _sceneLoadService = sceneLoadService;
            _cameraProvider = cameraProvider;
        }

        public void Enter()
        {
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
