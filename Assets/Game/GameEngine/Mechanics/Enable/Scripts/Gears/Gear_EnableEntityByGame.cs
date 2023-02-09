using Entities;
using GameSystem;
using UnityEngine;


public sealed class Gear_EnableEntityByGame : MonoBehaviour,
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private UnityEntity unit;

    void IGameStartElement.StartGame()
    {
        this.unit.Get<IComponent_Enable>().SetEnable(true);
    }

    void IGameFinishElement.FinishGame()
    {
        this.unit.Get<IComponent_Enable>().SetEnable(false);
    }
}