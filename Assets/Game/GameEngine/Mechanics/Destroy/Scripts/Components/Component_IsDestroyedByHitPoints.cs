using Sirenix.OdinInspector;
using UnityEngine;

[AddComponentMenu("GameEngine/Mechanics/Component «Is Destroyed» By Hit Points")]
public sealed class Component_IsDestroyedByHitPoints : MonoBehaviour, IComponent_IsDestroyed
{
    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    public bool IsDestroyed
    {
        get { return this.CheckIsDestroyed(); }
    }

    [Space]
    [SerializeField]
    private HitPointsEngine hitPointsEngine;

    private bool CheckIsDestroyed()
    {
        if (this.hitPointsEngine == null)
        {
            return default;
        }

        return this.hitPointsEngine.CurrentHitPoints <= 0;
    }

}