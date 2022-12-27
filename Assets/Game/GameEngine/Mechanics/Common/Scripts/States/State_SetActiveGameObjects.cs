using Elementary;
using UnityEngine;


public sealed class State_SetActiveGameObjects : State
{
    [SerializeField]
    private Mode mode;

    [SerializeField]
    private bool invertOnAwake;

    [SerializeField]
    private GameObject[] gameObjects;

    private void Awake()
    {
        if (this.invertOnAwake)
        {
            this.SetActiveGameObjects(this.mode != Mode.ENABLE);
        }
    }

    public override void Enter()
    {
        this.SetActiveGameObjects(this.mode == Mode.ENABLE);
    }

    public override void Exit()
    {
        this.SetActiveGameObjects(this.mode != Mode.ENABLE);
    }

    private void SetActiveGameObjects(bool isEnable)
    {
        for (int i = 0, count = this.gameObjects.Length; i < count; i++)
        {
            var gameObject = this.gameObjects[i];
            gameObject.SetActive(isEnable);
        }
    }

    private enum Mode
    {
        ENABLE = 0,
        DISABLE = 1
    }
}