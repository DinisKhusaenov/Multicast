using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.LogService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.Loading.Scene
{
    public class SceneLoader : ISceneLoadService
    {
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly ILogService _logService;

        public SceneLoader(ZenjectSceneLoader sceneLoader, ILogService logService)
        {
            _sceneLoader = sceneLoader;
            _logService = logService;
        }

        public async UniTask LoadScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == name
                && name != SceneNames.Initialize)
            {
                _logService.Log($"Reloading the scene {name}");
                onLoaded?.Invoke();
                return;
            }

            await LoadScenes(name, onLoaded);
        }

        private async UniTask LoadScenes(string sceneName, Action onLoaded = null, LoadSceneMode mode = LoadSceneMode.Single)
        {
            await _sceneLoader.LoadSceneAsync(sceneName, mode);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            onLoaded?.Invoke();
        }
    }
}
