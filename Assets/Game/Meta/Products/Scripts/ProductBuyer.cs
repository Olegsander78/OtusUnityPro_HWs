using Sirenix.OdinInspector;
using UnityEngine;

public sealed class ProductBuyer : MonoBehaviour, IConstructListener
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

    void IConstructListener.Construct(GameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
    }
}