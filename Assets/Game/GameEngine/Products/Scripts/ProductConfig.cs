using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


[CreateAssetMenu(
    fileName = "Product",
    menuName = "GameEngine/Products/New Product"
)]
public sealed class ProductConfig : SerializedScriptableObject
{
    public string Id
    {
        get { return this.origin.Id; }
    }

    public Product Prototype
    {
        get { return this.origin; }
    }

    [OdinSerialize]
    private Product origin = new();
}