using Infrastructure.Loading.Scene;
using UI.HUD.Windows;

namespace Infrastructure.States.States
{
    public class InitializeState : IState
    {
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ILoadingCurtain _loadingCurtain;

        public InitializeState(
            ApplicationStateMachine applicationStateMachine, 
            ISceneLoadService sceneLoadService, 
            ILoadingCurtain loadingCurtain)
        {
            _applicationStateMachine = applicationStateMachine;
            _sceneLoadService = sceneLoadService;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _sceneLoadService.LoadScene(SceneNames.Initialize, OnSceneLoaded);
        }

        public void Exit()
        {
        }

        private void OnSceneLoaded()
        {
            _loadingCurtain.Show();
            
            _applicationStateMachine.SwitchState<MenuState>();
        }
    }
}
