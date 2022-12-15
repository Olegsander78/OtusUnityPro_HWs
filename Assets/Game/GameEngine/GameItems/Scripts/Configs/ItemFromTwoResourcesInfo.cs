using UnityEngine;


[CreateAssetMenu(
    fileName = "ItemFromTwoResourcesInfo",
    menuName = "GameEngine/GameItems/New ItemFromTwoResourcesInfo"
)]
public sealed class ItemFromTwoResourcesInfo : ScriptableObject
{
    [SerializeField]
    public ItemType type;

    [SerializeField]
    public Sprite icon;

    [Header("Recipe")]
    [SerializeField]
    public ResourceType ResourceOne;

    [SerializeField]
    public int AmountResourceOne;

    [SerializeField]
    public ResourceType ResourceTwo;

    [SerializeField]
    public int AmountResourceTwo;

    [SerializeField]
    public GameObject PrefabItem;
}