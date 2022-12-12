using UnityEngine;


[AddComponentMenu("GameEngine/World Camera/World Camera Installer")]
public sealed class WorldCameraInstaller : MonoBehaviour
{
    private void Awake()
    {
        WorldCameraService.SetupCamera(Camera.main);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        WorldCameraService.SetupCamera(Camera.main);
    }
#endif
}