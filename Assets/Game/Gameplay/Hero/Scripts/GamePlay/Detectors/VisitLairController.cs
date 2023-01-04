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

    private IComponent_CollisionEvents _heroComponent;

    [SerializeField]
    private ScriptableEntityCondition _isLairCondition;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();
        _lairInteractor = context.GetService<LairInteractor>();
        _heroComponent = _hero.Get<IComponent_CollisionEvents>();
    }

    void IGameReadyElement.ReadyGame(IGameContext context)
    {
        _heroComponent.OnCollisionEntered += OnHeroEntered;
        _heroComponent.OnCollisionExited += OnHeroExited;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _heroComponent.OnCollisionEntered -= OnHeroEntered;
        _heroComponent.OnCollisionExited -= OnHeroExited;
    }

    private void OnHeroEntered(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IEntity entity) &&
            _isLairCondition.IsTrue(entity))
        {
            Debug.Log("Detect Lair");
            if (_lairInteractor.CanSpawnEnemy(entity))
            {
                _lairInteractor.StartSpawnEnemy(entity);
            }
        }
    }

    private void OnHeroExited(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IEntity entity) &&
            _isLairCondition.IsTrue(entity))
        {
            if (_lairInteractor.IsSpawningEnemy)
            {
                _lairInteractor.CancelSpawnEnemy();
            }
        }
    }
}
