using Elementary;
using UnityEngine;


public sealed class ZoneVisualAdapter : MonoBehaviour
{
    [SerializeField]
    private LimitedIntBehavior storage;

    [SerializeField]
    private ZoneVisual visualZone;

    private void Awake()
    {
        this.visualZone.SetupItems(this.storage.Value);
    }

    private void OnEnable()
    {
        this.storage.OnValueChanged += this.OnItemsChanged;
    }

    private void OnDisable()
    {
        this.storage.OnValueChanged -= this.OnItemsChanged;
    }

    private void OnItemsChanged(int count)
    {
        this.visualZone.SetupItems(count);
    }
    private void OnValidate()
    {
        if(this.visualZone != null && this.storage != null)
        {
            this.storage.OnValueChanged -= this.visualZone.SetupItems;
            this.storage.OnValueChanged += this.visualZone.SetupItems;
        }
    }
}