using Elementary;
using Entities;
using UnityEngine;

public class MakeDamageOnTriggerMechanic : MonoBehaviour
{
    public IntBehaviour Damage { get => _damage; set => value = _damage; }

    [SerializeField]
    private IntBehaviour _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IEntity>() != null)
        {
            if(other.GetComponent<IEntity>().Get<IComponent_TakeDamageMechanics>() != null)
            {  
                other.GetComponent<IEntity>().Get<IComponent_TakeDamageMechanics>().TakeDamage(_damage.Value);             
            }
        }        
    }
}
