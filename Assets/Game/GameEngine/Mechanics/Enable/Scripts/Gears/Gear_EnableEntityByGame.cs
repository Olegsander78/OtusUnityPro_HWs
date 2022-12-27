using Entities;
using GameElements;
using UnityEngine;


public sealed class Gear_EnableEntityByGame : MonoBehaviour,
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private UnityEntity unit;

    void IGameStartElement.StartGame(IGameContext context)
    {
        this.unit.Get<IComponent_Enable>().SetEnable(true);
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        this.unit.Get<IComponent_Enable>().SetEnable(false);
    }
}