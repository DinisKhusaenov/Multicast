using UnityEngine;

namespace Gameplay.Cameras
{
    public interface ICameraProvider
    {
        Camera MainCamera { get; }
        float WorldScreenHeight { get; }
        float WorldScreenWidth { get; }
        void SetMainCamera(Camera camera);
    }
}