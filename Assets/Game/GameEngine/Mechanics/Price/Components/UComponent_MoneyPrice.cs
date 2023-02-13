using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Price/Component «Money Price»")]
public sealed class UComponent_MoneyPrice : MonoBehaviour, IComponent_MoneyPrice
{
    public int Price
    {
        get { return this.price; }
    }

    [SerializeField]
    private int price;
}