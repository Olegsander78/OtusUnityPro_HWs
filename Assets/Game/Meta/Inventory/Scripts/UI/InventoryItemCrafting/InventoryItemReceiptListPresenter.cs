using System.Collections.Generic;
using GameSystem;
using UnityEngine;


public sealed class InventoryItemReceiptListPresenter : MonoBehaviour, IGameConstructElement
{
    [SerializeField]
    private InventoryItemReceiptCatalog receiptCatalog;

    [SerializeField]
    private InventoryItemReceiptView viewPrefab;

    [SerializeField]
    private Transform container;

    private InventoryItemCrafter craftManager;

    private StackableInventory inventory;

    private readonly List<InventoryItemReceiptPresenter> presenters = new();

    public void Show()
    {
        for (int i = 0, count = this.presenters.Count; i < count; i++)
        {
            var presenter = this.presenters[i];
            presenter.Start();
        }
    }

    public void Hide()
    {
        for (int i = 0, count = this.presenters.Count; i < count; i++)
        {
            var presenter = this.presenters[i];
            presenter.Stop();
        }
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        this.inventory = context.GetService<InventoryService>().GetInventory();
        this.craftManager = context.GetService<InventoryItemCrafter>();
        this.CreateReceipts();
    }

    private void CreateReceipts()
    {
        var receipts = this.receiptCatalog.GetAllReceipts();
        for (int i = 0, count = receipts.Length; i < count; i++)
        {
            var receipt = receipts[i];
            this.CreateReceipt(receipt);
        }
    }

    private void CreateReceipt(InventoryItemReceipt receipt)
    {
        var view = Instantiate(this.viewPrefab, this.container);
        var presenter = new InventoryItemReceiptPresenter(view, receipt);
        presenter.Construct(this.craftManager, this.inventory);
        this.presenters.Add(presenter);
    }
}