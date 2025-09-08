using Infrastructure.Loading.Scene;
using Infrastructure.SaveLoad;
using UI.HUD.Windows;

namespace Infrastructure.States.States
{
    public class InitializeState : IState
    {
        private readonly ApplicationStateMachine _applicationStateMachine;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IPersistentData _persistentData;
        private readonly IDataProvider _dataProvider;

        public InitializeState(
            ApplicationStateMachine applicationStateMachine, 
            ISceneLoadService sceneLoadService, 
            ILoadingCurtain loadingCurtain,
            IPersistentData persistentData,
            IDataProvider dataProvider)
        {
            _applicationStateMachine = applicationStateMachine;
            _sceneLoadService = sceneLoadService;
            _loadingCurtain = loadingCurtain;
            _persistentData = persistentData;
            _dataProvider = dataProvider;
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

            _persistentData.GameData = _dataProvider.Load<GameData>();
            _applicationStateMachine.SwitchState<MenuState>();
        }
    }
}
