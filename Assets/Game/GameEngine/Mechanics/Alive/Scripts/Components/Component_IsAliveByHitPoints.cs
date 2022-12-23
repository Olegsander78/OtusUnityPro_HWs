using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Is Alive» By Hit Points")]
public sealed class Component_IsAliveByHitPoints : MonoBehaviour, IComponent_IsAlive
{
    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    public bool IsAlive
    {
        get { return this.CheckIsAlive(); }
    }

    [Space]
    [SerializeField]
    private HitPointsEngine hitPointsEngine;

    private bool CheckIsAlive()
    {
        if (this.hitPointsEngine == null)
        {
            return default;
        }

        return this.hitPointsEngine.CurrentHitPoints > 0;
    }
}