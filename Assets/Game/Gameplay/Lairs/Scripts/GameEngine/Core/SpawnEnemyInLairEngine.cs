using Elementary;
using Entities;
using UnityEngine;

public class SpawnEnemyInLairEngine : MonoBehaviour
{
    public int CurrentEnemies
    {
        get { return _enemiesInSpawnZone.Value; }        
    }

    public int MaxEnemies
    {
        get { return _enemiesInSpawnZone.MaxValue; }
    }

    [Space]
    [SerializeField]
    private TimerBehaviour _workTimer;

    [SerializeField]
    private EventReceiver_Trigger _triggerZone;

    [SerializeField]
    private LimitedIntBehavior _enemiesInSpawnZone;

    [SerializeField]
    private UnityEntity _player;

    private void OnEnable()
    {
        _triggerZone.OnTriggerEntered += OnSpawnStart;
        _triggerZone.OnTriggerStaying += OnSpawnStart;
        _triggerZone.OnTriggerExited  += OnSpawnStop;
        _workTimer.OnFinished += OnSpawnFinishedOnTimer;
    }    

    private void OnDisable()
    {
        _triggerZone.OnTriggerEntered -= OnSpawnStart;
        _triggerZone.OnTriggerStaying -= OnSpawnStart;
        _triggerZone.OnTriggerExited -= OnSpawnStop;
        _workTimer.OnFinished -= OnSpawnFinishedOnTimer;
    }
    
    private void OnSpawnStart(Collider obj)
    {
        //if(obj.CompareTag("Player"))
        //{
        //    if (CanStartAppearance())
        //        StartAppearance();
        //}

        if(obj == _player.GetComponent<Collider>())
        {
            if (CanStartSpawn())
                StartSpawn();
        }
    }
    private bool CanStartSpawn()
    {
        if (_workTimer.IsPlaying)
        {
            return false;
        }

        if (_enemiesInSpawnZone.IsLimit)
        {
            return false;
        }

        return true;
    }
    private void StartSpawn()
    {
        _workTimer.ResetTime();
        _workTimer.Play();
    }
    private void OnSpawnStop(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            StopSpawn();
        }
    }

    private void StopSpawn()
    {
        _workTimer.Stop();
    }

    private void OnSpawnFinishedOnTimer()
    {
        _enemiesInSpawnZone.Value++;
    }
}
