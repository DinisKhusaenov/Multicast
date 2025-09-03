using System;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Loading.Scene
{
    public interface ISceneLoadService
    {
        UniTask LoadScene(string name, Action onLoaded = null);
    }
}
