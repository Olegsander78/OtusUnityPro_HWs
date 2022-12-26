using System;
using Entities;
using GameElements;
using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class Gear_RespawnEntityByTimer : MonoBehaviour,
    IGameReadyElement,
    IGameFinishElement
{
    [SerializeField]
    private UnityEntity entity;

    [Space]
    [SerializeField]
    private TriggerType trigger;

    [SerializeField]
    private FloatAdapter respawnTime;

    private Timer respawnTimer;

    [SerializeField]
    private bool hasRespawnPoint;

    [ShowIf("hasRespawnPoint")]
    [SerializeField]
    private Transform respawnPoint;

    private void Awake()
    {
        this.respawnTimer = new Timer(this, this.respawnTime.Value);
    }

    void IGameReadyElement.ReadyGame(IGameContext context)
    {
        if (this.trigger == TriggerType.ENTITY_DESTROYED)
        {
            this.entity.Get<IComponent_OnDestroyed>().OnDestroyed += this.OnEntityDestroyed;
        }

        if (this.trigger == TriggerType.ENTITY_DEACTIVATE)
        {
            this.entity.Get<IComponent_Active>().OnDeactivate += this.OnEntityDeactivated;
        }

        if (this.trigger == TriggerType.ENTITY_DISABLED)
        {
            this.entity.Get<IComponent_Enable>().OnEnabled += this.OnEntityEnabled;
        }

        this.respawnTimer.OnFinished += this.OnTimerFinished;
    }


    void IGameFinishElement.FinishGame(IGameContext context)
    {
        if (this.trigger == TriggerType.ENTITY_DESTROYED)
        {
            this.entity.Get<IComponent_OnDestroyed>().OnDestroyed -= this.OnEntityDestroyed;
        }

        if (this.trigger == TriggerType.ENTITY_DEACTIVATE)
        {
            this.entity.Get<IComponent_Active>().OnDeactivate -= this.OnEntityDeactivated;
        }

        if (this.trigger == TriggerType.ENTITY_DISABLED)
        {
            this.entity.Get<IComponent_Enable>().OnEnabled -= this.OnEntityEnabled;
        }

        this.respawnTimer.OnFinished -= this.OnTimerFinished;
        this.respawnTimer.Stop();
    }

    private void OnEntityDeactivated()
    {
        this.StartTimer();
    }

    private void OnEntityDestroyed(DestroyEvent destroyEvent)
    {
        this.StartTimer();
    }

    private void OnEntityEnabled(bool isEnable)
    {
        if (!isEnable)
        {
            this.StartTimer();
        }
    }

    private void OnTimerFinished()
    {
        this.RespawnEntity();
    }

    private void StartTimer()
    {
        this.respawnTimer.Stop();
        this.respawnTimer.ResetTime();
        this.respawnTimer.Play();
    }

    private void RespawnEntity()
    {
        if (this.hasRespawnPoint)
        {
            var positionComponent = this.entity.Get<IComponent_SetPosition>();
            positionComponent.SetPosition(this.respawnPoint.position);

            var rotationComponent = this.entity.Get<IComponent_SetRotation>();
            rotationComponent.SetRotation(this.respawnPoint.rotation);
        }

        this.entity.Get<IComponent_Respawn>().Respawn();
    }

#if UNITY_EDITOR
    private void Reset()
    {
        this.hasRespawnPoint = true;
        this.respawnPoint = this.transform;
    }
#endif

    private enum TriggerType
    {
        ENTITY_DESTROYED = 0,
        ENTITY_DISABLED = 1,
        ENTITY_DEACTIVATE = 2
    }
}