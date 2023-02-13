using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "ProductCatalog",
    menuName = "GameEngine/Products/New ProductCatalog"
)]
public sealed class ProductCatalog : ScriptableObject
{
    public int ProductCount
    {
        get { return this.products.Length; }
    }

    [SerializeField]
    private ProductConfig[] products;

    public ProductConfig GetProduct(int index)
    {
        return this.products[index];
    }

    public ProductConfig FindProduct(string id)
    {
        for (int i = 0, count = this.products.Length; i < count; i++)
        {
            var item = this.products[i];
            if (item.Id == id)
            {
                return item;
            }
        }

        throw new Exception($"Product with id {id} is not found!");
    }

    public ProductConfig[] GetAllProducts()
    {
        return this.products;
    }
}