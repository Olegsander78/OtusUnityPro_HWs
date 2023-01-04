using Entities;
using GameElements;
using System;
using UnityEngine;

[AddComponentMenu("Gameplay/Hero/Hero Interactor «Spawn Enemies from Lair»")]
public class LairInteractor : MonoBehaviour, IGameInitElement
{
    public event Action<IEntity> OnSpawnCompleted;
    public bool IsSpawningEnemy { get; private set; }

    [SerializeField]
    private float delay = 0.15f;

    [SerializeField]
    private float _duration = 5f;

    private IEntity _currentLair;

    private ResourceStorage _resourceStorage;

    private IComponent_TriggerEvents _heroTriggerComponent;

    private Coroutine delayCoroutine;

    private Coroutine _spawnEnemyCoroutine;

    [SerializeField]
    private ScriptableEntityCondition _isLairActive;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _heroTriggerComponent = context
            .GetService<HeroService>()
            .GetHero()
            .Get<IComponent_TriggerEvents>();
    }

    //public void TryStartHarvest(IEntity resourceObject)
    //{
    //    if (this.heroComponent.IsHarvesting)
    //    {
    //        return;
    //    }

    //    if (this.delayCoroutine == null)
    //    {
    //        this.delayCoroutine = this.StartCoroutine(this.HarvestRoutine(resourceObject));
    //    }
    //}

    //private IEnumerator HarvestRoutine(IEntity resourceObject)
    //{
    //    yield return new WaitForSeconds(this.delay);

    //    var operation = new HarvestResourceOperation(resourceObject);
    //    if (this.heroComponent.CanStartHarvest(operation))
    //    {
    //        this.heroComponent.StartHarvest(operation);
    //    }
    //    Debug.Log($"Start Harvest {resourceObject}");
    //    this.delayCoroutine = null;
    //}

    //internal bool CanHarvest(IEntity resource)
    //{
    //    if (!_isLairActive.IsTrue(resource))
    //        return false;

    //    return true;
    //}

    //internal void StartHarvest(IEntity resource)
    //{
    //    if (!CanHarvest(resource))
    //    {
    //        Debug.LogWarning($"Can't harvest {resource}");
    //        return;
    //    }

    //    Debug.LogWarning($"Start harvest {resource}");

    //    IsHarvesting = true;
    //    _currentLair = resource;
    //    _spawnEnemyCoroutine = StartCoroutine(StartHarvestRoutine());
    //}

    //private IEnumerator StartHarvestRoutine()
    //{
    //    yield return new WaitForSeconds(_duration);

    //    var resource = _currentLair;
    //    DestroyResource(resource);
    //    AddResourcesToStorage(resource);
    //    ResetState();

    //    Debug.LogWarning($"Completed harvest {resource}");
    //    OnHarvestCompleted?.Invoke(resource);
    //}

    //private void ResetState()
    //{
    //    _currentLair = null;
    //    IsHarvesting = false;
    //    _spawnEnemyCoroutine = null;
    //}
    //internal void CancelHarvest()
    //{
    //    if (IsHarvesting)
    //    {
    //        StopCoroutine(_spawnEnemyCoroutine);
    //        ResetState();
    //        Debug.Log("Cancel harvest resource");
    //    }
    //}

    //private void DestroyResource(IEntity resource)
    //{
    //    resource.Get<IComponent_Collect>().Collect();
    //}
    //private void AddResourcesToStorage(IEntity resource)
    //{
    //    var resourceType = resource.Get<IComponent_GetResourceType>().ResourceType;
    //    var resourceAmount = resource.Get<IComponent_GetResourceCount>().ResourceCount;
    //    _resourceStorage.AddResource(resourceType, resourceAmount);
    //}


    internal void CancelSpawnEnemy()
    {
        Debug.Log("Cancel spawn Enemy!");
    }

    internal bool CanSpawnEnemy(IEntity entity)
    {
        return true;
    }

    internal void StartSpawnEnemy(IEntity entity)
    {
        Debug.Log("Start spawn Enemy!");
    }
}
