using UnityEngine;


[AddComponentMenu("Gameplay/Conveyors/Component «Conveyor Id»")]
public sealed class Component_ConveyorId : MonoBehaviour, IComponent_Id
{
    public string Id { get; set; }
}