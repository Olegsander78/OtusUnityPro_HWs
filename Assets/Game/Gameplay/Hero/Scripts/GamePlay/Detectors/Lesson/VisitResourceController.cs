using Entities;
using UnityEngine;
using GameSystem;

public sealed class VisitResourceController : MonoBehaviour,
    IGameConstructElement,
    //IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{   
    private IEntity _hero;

    private HarvestResourceInteractor _harvestInteractor;

    private IComponent_CollisionEvents _heroComponent;

    [SerializeField]
    private ScriptableEntityCondition _isResourceCondition;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();
        _harvestInteractor = context.GetService<HarvestResourceInteractor>();
        _heroComponent = _hero.Get<IComponent_CollisionEvents>();
    }

    void IGameReadyElement.ReadyGame()
    {
        _heroComponent.OnCollisionEntered += OnHeroEntered;
        _heroComponent.OnCollisionExited += OnHeroExited;
    }

    void IGameFinishElement.FinishGame()
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
            if (_harvestInteractor.IsHarvesting)
            {                
                _harvestInteractor.CancelHarvest();
            }
        }
    }


}