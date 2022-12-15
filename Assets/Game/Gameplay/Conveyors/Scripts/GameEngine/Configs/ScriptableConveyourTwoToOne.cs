using UnityEngine;


[CreateAssetMenu(
    fileName = "ScriptableConveyor",
    menuName = "Gameplay/Conveyors/New ScriptableConveyorTwoToOne"
)]
public sealed class ScriptableConveyourTwoToOne : ScriptableObject
{
    [SerializeField]
    public string id;

    [Header("Load Zone One")]
    [SerializeField]
    public ResourceType inputOneResourceType;

    [SerializeField]
    public int inputOneCapacity;

    [Header("Load Zone Two")]
    [SerializeField]
    public ResourceType inputTwoResourceType;    

    [SerializeField]
    public int inputTwoCapacity;

    [Header("Unload Zone")]
    [SerializeField]
    public ItemFromTwoResourcesInfo outputItem;
    
    [SerializeField]
    public int outputCapacity;

    [Header("Work")]
    [SerializeField]
    public float workTime;
}