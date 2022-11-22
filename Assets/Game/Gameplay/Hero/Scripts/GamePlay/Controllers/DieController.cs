using UnityEngine;
using Entities;
using Sirenix.OdinInspector;

[AddComponentMenu("Gameplay/Hero/Hero Die Controller")]
public class DieController : MonoBehaviour,
    IStartGameListener,
    IFinishGameListener
{
    [SerializeField]
    private UnityEntityBase _unit;

    private IComponent_Die _dieComponent;

    private IComponent_MoveOnPosition _respawnComponent;

    [SerializeField]
    private Transform _respawnPoint;

    private void Awake()
    {
        _dieComponent = _unit.Get<IComponent_Die>();
        _respawnComponent = _unit.Get<IComponent_MoveOnPosition>();
    }


    void IStartGameListener.OnStartGame()
    {
        _dieComponent.OnDieEvent += OnHeroDestroyed;
    }

    void IFinishGameListener.OnFinishGame()
    {
        _dieComponent.OnDieEvent -= OnHeroDestroyed;
    }

    [Button]
    private void OnHeroDestroyed()
    {
        _respawnComponent.Move(_respawnPoint.position);
    }
}

