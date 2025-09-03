using UnityEngine;

namespace UI.HUD.Windows
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private Canvas _canvas;
        
        public void Show()
        {
            _canvas.enabled = true;
        }
        
        public void Hide()
        {
            _canvas.enabled = false;
        }
    }
}