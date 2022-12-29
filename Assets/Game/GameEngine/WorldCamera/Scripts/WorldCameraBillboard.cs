using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/World Camera/World Camera Billboard")]
[ExecuteAlways]
public sealed class WorldCameraBillboard : MonoBehaviour
{
    [SerializeField]
    private bool lookAtStart;

    private void Start()
    {
        if (this.lookAtStart)
        {
            this.LookAtCamera();
        }
    }

    [Button]
    public void LookAtCamera()
    {
        var rootPosition = this.transform.position;
        var instance = WorldCamera.Instance;
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