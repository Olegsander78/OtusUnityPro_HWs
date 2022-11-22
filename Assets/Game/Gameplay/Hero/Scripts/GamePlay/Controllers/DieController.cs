using UnityEngine;
using Entities;

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
        _dieComponent.OnDie += OnHeroDestroyed;
    }

    void IFinishGameListener.OnFinishGame()
    {
        _dieComponent.OnDie -= OnHeroDestroyed;
    }

    private void OnHeroDestroyed()
    {
        _respawnComponent.Move(_respawnPoint.position);
    }
}

