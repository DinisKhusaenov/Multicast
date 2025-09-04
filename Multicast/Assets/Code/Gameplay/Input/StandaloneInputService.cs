using Gameplay.Cameras;
using UnityEngine;

namespace Gameplay.Input
{
    public class StandaloneInputService : IInputService
    {
        private static Vector3 DefaultPosition = new Vector3(999, 999, 0);
        
        private readonly ICameraProvider _cameraProvider;
        private Camera _mainCamera;

        public StandaloneInputService(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }
        
        public Vector3 GetTouchPosition()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                Vector3 mousePosition = UnityEngine.Input.mousePosition;
                
                return _cameraProvider.MainCamera.
                    ScreenToWorldPoint(new Vector3(
                        mousePosition.x, 
                        mousePosition.y, 
                        0));
            }
            else
            {
                return DefaultPosition;
            }
        }
    }
}