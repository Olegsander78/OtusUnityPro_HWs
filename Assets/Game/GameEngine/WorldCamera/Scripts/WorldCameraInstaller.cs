using UnityEngine;


[AddComponentMenu("GameEngine/World Camera/World Camera Installer")]
public sealed class WorldCameraInstaller : MonoBehaviour
{
    private void Awake()
    {
        WorldCamera.Setup(Camera.main);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        WorldCamera.Setup(Camera.main);
    }
#endif
}