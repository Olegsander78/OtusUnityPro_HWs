using Elementary;
using UnityEngine;


public sealed class AnimationStateComposite : MonoBehaviour
{
    [Space]
    [SerializeField]
    private string enterKey;

    [SerializeField]
    private string exitKey;

    [Space]
    [SerializeField]
    private AnimationSystem system;

    [SerializeField]
    private MonoState[] states;

    private void OnEnable()
    {
        this.system.OnStringReceived += this.OnEventReceived;
    }

    private void OnDisable()
    {
        this.system.OnStringReceived -= this.OnEventReceived;
    }

    private void OnEventReceived(string message)
    {
        if (message == this.enterKey)
        {
            this.EnterState();
        }

        if (message == this.exitKey)
        {
            this.ExitState();
        }
    }

    private void EnterState()
    {
        for (int i = 0, count = this.states.Length; i < count; i++)
        {
            var state = this.states[i];
            state.Enter();
        }
    }

    private void ExitState()
    {
        for (int i = 0, count = this.states.Length; i < count; i++)
        {
            var state = this.states[i];
            state.Exit();
        }
    }
}