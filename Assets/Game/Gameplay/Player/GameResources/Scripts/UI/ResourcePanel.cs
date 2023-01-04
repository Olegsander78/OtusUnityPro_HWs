using UnityEngine;

public sealed class ResourcePanel : NumberItemDictionary<ResourceType>
{
    [Space]
    [SerializeField]
    private ResourceInfoCatalog resourceCatalog;

    private void Awake()
    {
        this.CreateItems();
    }

    private void CreateItems()
    {
        var resourceConfigs = this.resourceCatalog.GetAllResources();
        for (int i = 0, count = resourceConfigs.Length; i < count; i++)
        {
            var config = resourceConfigs[i];
            this.AddItem(config.type, 0);
        }
    }

    protected override Sprite FindIcon(ResourceType key)
    {
        var resourceInfo = this.resourceCatalog.FindResource(key);
        return resourceInfo.icon;
    }
}