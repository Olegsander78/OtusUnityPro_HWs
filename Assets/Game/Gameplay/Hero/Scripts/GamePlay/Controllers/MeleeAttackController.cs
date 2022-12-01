using UnityEngine;
using Entities;

[AddComponentMenu("Gameplay/Hero/Hero MeleeAttack Controller")]
public class MeleeAttackController : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
   
    private KeyboardInput _input;
    
    private UnityEntityBase _unit;

    [SerializeField]
    private UnityEntityBase _target;

    [SerializeField]
    private LayerMask _layerMask;

    private IComponent_MeleeAttack _meleeAttackComponent;

    void IConstructListener.Construct(GameContext context)
    {
        _input = context.GetService<KeyboardInput>();

        _unit = (UnityEntityBase)context.GetService<HeroService>().GetHero();

        _meleeAttackComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_MeleeAttack>();
    }

    void IStartGameListener.OnStartGame()
    {
        _input.OnMeleeAttackEvent += TryMeleeAttack;
    }

    void IFinishGameListener.OnFinishGame()
    {
        _input.OnMeleeAttackEvent -= TryMeleeAttack;
    }

    void TryMeleeAttack()
    {
        Ray ray = new Ray(_unit.transform.position + Vector3.up, _unit.transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, 1f, 1f, _layerMask);
        
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
              hit.collider.TryGetComponent(out _target);
                break;
            }
            if (_target != null)
                MeleeAttack(_target);
        }
        else
        {
            //Debug.Log("Missed! Try to get closer!");
            _target = null;
            return;
            //throw new Exception("Enemy is not founded!");
        }        
    }
    private void MeleeAttack(UnityEntityBase target)
    {
        _meleeAttackComponent.Attack(target);
    }
}
