using Entities;
using UnityEngine;
using GameElements;

public class VisitLairController : MonoBehaviour,
    IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{
    private IEntity _hero;

    private LairInteractor _lairInteractor;

    private IComponent_TriggerEvents _heroTriggerComponent;

    [SerializeField]
    private ScriptableEntityCondition _isLairCondition;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();
        _lairInteractor = context.GetService<LairInteractor>();
        _heroTriggerComponent = _hero.Get<IComponent_TriggerEvents>();
    }

    void IGameReadyElement.ReadyGame(IGameContext context)
    {
        _heroTriggerComponent.OnEntered += OnHeroEntered;
        _heroTriggerComponent.OnExited += OnHeroExited;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _heroTriggerComponent.OnEntered -= OnHeroEntered;
        _heroTriggerComponent.OnExited -= OnHeroExited;
    }

    private void OnHeroEntered(Collider other)
    {
        if (other.TryGetComponent(out IEntity entity) &&
            _isLairCondition.IsTrue(entity))
        {
            Debug.Log("Detect Lair");
            if (_lairInteractor.CanSpawnEnemy(entity))
            {
                _lairInteractor.StartSpawnEnemy(entity);
            }
        }
    }

    private void OnHeroExited(Collider other)
    {
        if (other.TryGetComponent(out IEntity entity) &&
            _isLairCondition.IsTrue(entity))
        {
            if (_lairInteractor.IsSpawningEnemy)
            {
                _lairInteractor.CancelSpawnEnemy();
            }
        }
    }
}
