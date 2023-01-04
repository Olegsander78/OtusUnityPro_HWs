using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ResoucePanelAdapter : MonoBehaviour,
    IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{
    [SerializeField]
    private bool listenIncome = false;

    [SerializeField]
    private bool listenSpend = true;

    [SerializeField]
    private ResourcePanel panel;

    private ResourceStorage resourceStorage;

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.resourceStorage = context.GetService<ResourceStorage>();
        this.SetupPanel();
    }

    void IGameReadyElement.ReadyGame(IGameContext context)
    {
        if (this.listenIncome)
        {
            this.resourceStorage.OnResourceAdded += this.OnResourceAdded;
        }

        if (this.listenSpend)
        {
            this.resourceStorage.OnResourceExtracted += this.OnResourceExtracted;
        }
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        this.resourceStorage.OnResourceExtracted -= this.OnResourceExtracted;
        this.resourceStorage.OnResourceAdded -= this.OnResourceAdded;
    }

    private void SetupPanel()
    {
        var resources = this.resourceStorage.GetAllResources();
        for (var i = 0; i < resources.Length; i++)
        {
            var resource = resources[i];
            this.panel.SetupItem(resource.type, resource.amount);
        }
    }

    private void OnResourceAdded(ResourceType type, int range)
    {
        this.panel.IncrementItem(type, range);
    }

    private void OnResourceExtracted(ResourceType type, int range)
    {
        this.panel.DecrementItem(type, range);
    }

#if UNITY_EDITOR

    [Title("Debug")]
    [Button("Sync Resources")]
    private void Editor_SyncResources()
    {
        this.SetupPanel();
    }
#endif
}