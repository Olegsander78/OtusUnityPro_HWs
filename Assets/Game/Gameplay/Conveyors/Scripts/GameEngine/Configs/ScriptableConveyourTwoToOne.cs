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

    [SerializeField]
    public DoubleWorkOnLoadEngine.RecipeTwoPart outputItem;


    //[Header("Unload Zone")]
    //[SerializeField]
    //public ResourceType outputResourceType;

    [SerializeField]
    public int outputCapacity;

    [Header("Work")]
    [SerializeField]
    public float workTime;
}