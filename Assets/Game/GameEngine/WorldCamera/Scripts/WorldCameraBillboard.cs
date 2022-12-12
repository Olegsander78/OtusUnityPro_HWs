using UnityEngine;


[AddComponentMenu("GameEngine/World Camera/World Camera Billboard")]
public sealed class WorldCameraBillboard : MonoBehaviour
{
    private void Start()
    {
        this.LookAtCamera();
    }

    private void LookAtCamera()
    {
        var rootPosition = this.transform.position;
        var instance = WorldCameraService.Instance;
        if (ReferenceEquals(instance, null))
        {
            return;
        }

        var cameraRotation = instance.transform.rotation;
        var cameraVector = cameraRotation * Vector3.forward;
        var worldUp = cameraRotation * Vector3.up;
        this.transform.LookAt(rootPosition + cameraVector, worldUp);
    }
}