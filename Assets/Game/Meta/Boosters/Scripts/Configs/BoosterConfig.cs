using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(
    fileName = "BoosterConfig",
    menuName = BoosterExtensions.MENU_PATH + "New BoosterConfig"
)]
public abstract class BoosterConfig : SerializedScriptableObject
{
    [SerializeField]
    public string id;

    [Space]
    [Tooltip("Duration in seconds")]
    [SerializeField]
    public float duration;

    [Space]
    [SerializeField]
    public BoosterMetadata metadata;

    public abstract Booster InstantiateBooster(MonoBehaviour context);
}