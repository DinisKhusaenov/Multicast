using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.HUD.Windows.Factory
{
    public interface IWindowFactory
    {
        UniTask<IGameOverView> CreateGameOverView(Canvas canvas);
    }
}