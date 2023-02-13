using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Equip/Component «Get Equip Type»")]
public sealed class UComponent_EquipType : MonoBehaviour, IComponent_GetEqupType
{
    public EquipType Type
    {
        get { return this.type; }
    }

    [SerializeField]
    private EquipType type;
}