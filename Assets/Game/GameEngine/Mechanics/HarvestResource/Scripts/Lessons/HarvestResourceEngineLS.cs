using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class HarvestResourceEngineLS : MonoBehaviour
{


    





    //StopCoroutine(_harvestCoroutine);

    //_harvestCoroutine= null;



    public event Action<HarvestResourceOperation> OnStarted;

    public event Action<HarvestResourceOperation> OnStopped;
    public bool IsHarvesting
    {
        get
        {
            return _timerCoroutine != null;
        }
    }

    [SerializeField]
    private float _duration = 5f;

    private HarvestResourceOperation _operation;

    private Coroutine _timerCoroutine;


    //public HarvestResourceOperation CurrentOperation
    //{
    //    get { return this.operation; }
    //}

    //private HarvestResourceOperation operation;

    //[SerializeReference]
    //private List<IHarvestResourceCondition> preconditions = new();

    //[SerializeReference]
    //private List<IHarvestResourceAction> preactions = new();

    public bool CanStartHarvest(HarvestResourceOperation operation)
    {
        //if (this.IsHarvesting)
        //{
        //    return false;
        //}

        //for (int i = 0, count = this.preconditions.Count; i < count; i++)
        //{
        //    var conditions = this.preconditions[i];
        //    if (!conditions.IsTrue(operation))
        //    {
        //        return false;
        //    }
        //}

        return !IsHarvesting;
    }

    public void StartHarvest(HarvestResourceOperation operation)
    {
        if (IsHarvesting)
            return;
        
        _operation = operation;
        OnStarted?.Invoke(operation);
        _timerCoroutine = StartCoroutine(StartHarvestRoutine());

        //if (!this.CanStartHarvest(operation))
        //{
        //    return;
        //}

        //this.DoPreactions(operation);
        //Debug.Log("Engine: start");
        //this.operation = operation;
        //this.OnStarted?.Invoke(operation);
    }

    private IEnumerator StartHarvestRoutine()
    {
        yield return new WaitForSeconds(_duration);
        _operation.IsCompleted= true;
        _timerCoroutine= null;
        OnStopped?.Invoke(_operation);
    }

    public void StopHarvest()
    {
        if(IsHarvesting)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
            OnStopped?.Invoke(_operation);
        }
        
        //if (this.IsHarvesting)
        //{
        //    Debug.Log("Engine: stop");
        //    var operation = this.operation;
        //    this.operation = null;
        //    this.OnStopped?.Invoke(operation);
        //}
    }

    //[Button]
    //private void Complete()
    //{
    //    if (this.IsHarvesting)
    //    {
    //        this.operation.isCompleted = true;
    //        this.StopHarvest();
    //    }
    //}

    //private void DoPreactions(HarvestResourceOperation operation)
    //{
    //    for (int i = 0, count = this.preactions.Count; i < count; i++)
    //    {
    //        var action = this.preactions[i];
    //        action.Do(operation);
    //    }
    //}
}