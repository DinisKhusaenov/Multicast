using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameLogic.Gameplay.GameLogic;
using Gameplay.Clusters;
using Gameplay.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD.Service
{
    public interface IHUDService : IDisposable
    {
        void Initialize(ILevelSessionService levelSessionService, Canvas canvas, Button quitButton);
        void InitializeByLevel(IReadOnlyList<IClusterContainer> containers);
        UniTask ShowGameOverView();
        event Action OnQuitClicked;
    }
}