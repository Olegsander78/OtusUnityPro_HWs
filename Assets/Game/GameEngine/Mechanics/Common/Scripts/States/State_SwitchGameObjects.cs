using Elementary;
using UnityEngine;


public sealed class State_SwitchGameObjects : State
{
    [SerializeField]
    private bool disableOnAwake = true;

    [Space]
    [SerializeField]
    private GameObject[] enableObjects;

    [Space]
    [SerializeField]
    private GameObject[] disableObjects;

    private void Awake()
    {
        if (this.disableOnAwake)
        {
            this.SwitchObjects(false);
        }
    }

    public override void Enter()
    {
        this.SwitchObjects(true);
    }

    public override void Exit()
    {
        this.SwitchObjects(false);
    }

    private void SwitchObjects(bool enabled)
    {
        for (int i = 0, count = this.enableObjects.Length; i < count; i++)
        {
            var go = this.enableObjects[i];
            if (go != null)
            {
                go.SetActive(enabled);
            }
        }

        var disabled = !enabled;
        for (int i = 0, count = this.disableObjects.Length; i < count; i++)
        {
            var go = this.disableObjects[i];
            if (go != null)
            {
                go.SetActive(disabled);
            }
        }
    }
}