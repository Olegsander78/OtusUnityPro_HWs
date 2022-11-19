using UnityEngine;
using Entities;
using System;

[AddComponentMenu("Gameplay/Hero/Hero MeleeAttack Controller")]
public class MeleeAttackController : MonoBehaviour
{
    [SerializeField]
    private UnityEntityBase _unit;

    [SerializeField]
    private UnityEntityBase _enemy;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private IComponent_MeleeAttack _meleeAttackComponent;

    private void Awake()
    {
        _meleeAttackComponent = _unit.Get<IComponent_MeleeAttack>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
            TryMeleeAttack();
    }    

    void TryMeleeAttack()
    {
        Ray ray = new Ray(_unit.transform.position + Vector3.up, _unit.transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, 1f, 1f, _layerMask);
        
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
              _enemy= hit.collider.GetComponent<UnityEntityBase>();
                break;
            }

            MeleeAttack(_enemy);
        }
        else
        {
            Debug.Log("Missed! Try to get closer!");
            _enemy = null;
            return;
            //throw new Exception("Enemy is not founded!");
        }        
    }
    private void MeleeAttack(UnityEntityBase enemy)
    {
        _meleeAttackComponent.Attack(enemy);
    }
}
