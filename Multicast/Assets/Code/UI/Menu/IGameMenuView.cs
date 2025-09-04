using System;
using UnityEngine;

namespace UI.Menu
{
    public interface IGameMenuView
    {
        event Action StartClicked;
        
        Transform transform { get; }
        void SetLevel(int level);
    }
}