using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "InventoryItemReceiptCatalog",
    menuName = "Meta/Inventory/New InventoryItemReceiptCatalog"
)]
public sealed class InventoryItemReceiptCatalog : ScriptableObject
{
    [SerializeField]
    private InventoryItemReceipt[] receipts;

    public InventoryItemReceipt[] GetAllReceipts()
    {
        return this.receipts;
    }

    public InventoryItemReceipt FindReceipt(string name)
    {
        for (int i = 0, count = this.receipts.Length; i < count; i++)
        {
            var receipt = this.receipts[i];
            if (receipt.resultInfo.ItemName == name)
            {
                return receipt;
            }
        }

        throw new Exception($"Receipt with name {name} is not found!");
    }
}