using UnityEngine;


public static class WorldCamera
{
    public static Camera Instance { get; private set; }

    public static void Setup(Camera camera)
    {
        Instance = camera;
    }
}