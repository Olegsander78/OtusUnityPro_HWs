using GameSystem;
using UnityEngine;
using static GameSystem.GameComponentType;


public sealed class PlayerVendorSystemInstaller : MonoGameInstaller
{
    [GameComponent(SERVICE)]
    [SerializeField]
    private VendorSaleInteractor vendorInteractor = new();
}