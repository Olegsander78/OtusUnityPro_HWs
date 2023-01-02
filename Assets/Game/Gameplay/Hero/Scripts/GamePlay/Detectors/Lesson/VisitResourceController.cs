using Entities;
using UnityEngine;
using GameElements;

public sealed class VisitResourceController : MonoBehaviour,
    IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{   
    private IEntity _hero;

    private HarvestResourceInteractor _harvestInteractor;

    private IComponent_CollisionEvents _heroComponent;

    [SerializeField]
    private ScriptableEntityCondition _isResourceCondition;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();
        _harvestInteractor = context.GetService<HarvestResourceInteractor>();
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
            _isResourceCondition.IsTrue(entity))
        {
            Debug.Log("Detect resource");
            //_harvestInteractor.TryStartHarvest(entity);
            if (_harvestInteractor.CanHarvest(entity))
            {
                _harvestInteractor.StartHarvest(entity);
            }
        }
    }

    private void OnHeroExited(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IEntity entity) &&
            _isResourceCondition.IsTrue(entity))
        {
            //if (_hero.Get<IComponent_HarvestResource>().IsHarvesting)
            //{
            //    Debug.Log("Stop harvest resource");
            //    _hero.Get<IComponent_HarvestResource>().StopHarvest();
            //}
        }
    }
}