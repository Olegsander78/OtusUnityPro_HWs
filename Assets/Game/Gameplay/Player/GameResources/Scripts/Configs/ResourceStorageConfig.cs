using UnityEngine;


[CreateAssetMenu(
    fileName = "ResourceStorageConfig",
    menuName = "Gameplay/Player/New ResourceStorageConfig"
)]
public sealed class ResourceStorageConfig : ScriptableObject
{
    private const string CONFIG_PATH = "PlayerGameResourcesConfig";

    public ResourceData[] InitialResources
    {
        get { return this.initialResources; }
    }

    [SerializeField]
    private ResourceData[] initialResources;

    public static ResourceStorageConfig LoadAsset()
    {
        return Resources.Load<ResourceStorageConfig>(CONFIG_PATH);
    }
}