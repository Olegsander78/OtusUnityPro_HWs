using UnityEngine;


[CreateAssetMenu(
    fileName = "ScriptableConveyor",
    menuName = "Gameplay/Conveyors/New ScriptableConveyorOneToOne"
)]
public sealed class ScriptableConveyourOneToOne : ScriptableObject
{
    [SerializeField]
    public string id;

    [Header("Load Zone")]
    [SerializeField]
    public ResourceType inputResourceType;

    [SerializeField]
    public int inputCapacity;

    [Header("Unload Zone")]
    [SerializeField]
    public ResourceType outputResourceType;

    [SerializeField]
    public int outputCapacity;

    [Header("Work")]
    [SerializeField]
    public float workTime;
}