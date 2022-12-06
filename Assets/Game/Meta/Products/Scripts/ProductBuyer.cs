using Sirenix.OdinInspector;
using UnityEngine;
using GameElements;

public sealed class ProductBuyer : MonoBehaviour, IGameInitElement
{
    private MoneyStorage _moneyStorage;

    [Button]
    public bool CanBuy(Product product)
    {
        return _moneyStorage.Money >= product.price;
    }

    [Button]
    public void Buy(Product product)
    {
        if (CanBuy(product))
        {
            _moneyStorage.SpendMoney(product.price);
            Debug.Log($"<color=green>Product {product.title} successfully purchased!</color>");
        }
        else
        {
            Debug.LogWarning($"<color=red>Not enough money for product {product.title}!</color>");
        }
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
    }
}