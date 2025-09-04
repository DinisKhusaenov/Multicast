using Gameplay.Cameras;
using UnityEngine;

namespace Gameplay.Input
{
    public class MobileInputService : IInputService
    {
        private static Vector3 DefaultPosition = new Vector3(999, 999, 0);
        
        private readonly ICameraProvider _cameraProvider;

        public MobileInputService(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            
        }
        public Vector3 GetTouchPosition()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                Vector3 mousePosition = UnityEngine.Input.GetTouch(0).position;
                
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