using System;
using System.Collections.Generic;

namespace UI.HUD.Windows
{
    public interface IGameOverView
    {
        event Action NextLevelClicked;
        event Action MenuClicked;

        void Show(List<string> words);
        void Hide();
    }
}