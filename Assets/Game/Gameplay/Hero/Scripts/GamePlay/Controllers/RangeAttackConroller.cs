using UnityEngine;
using Entities;

[AddComponentMenu("Gameplay/Hero/Hero RangeAttack Controller")]
public class RangeAttackConroller : MonoBehaviour
{
    [SerializeField]
    private UnityEntityBase _unit;

    [SerializeField]
    private IComponent_RangeAttack _rangeAttackComponent;

    private void Awake()
    {
        _rangeAttackComponent = _unit.Get<IComponent_RangeAttack>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
            RangeAttack();
    }

    private void RangeAttack()
    {
        _rangeAttackComponent.Attack();
    }
}
