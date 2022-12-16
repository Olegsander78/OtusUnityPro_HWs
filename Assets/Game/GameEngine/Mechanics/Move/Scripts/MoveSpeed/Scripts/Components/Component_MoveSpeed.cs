using System;
using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Move Speed»")]
public sealed class Component_MoveSpeed : MonoBehaviour,
    IComponent_GetMoveSpeed,
    IComponent_SetMoveSpeed,
    IComponent_OnMoveSpeedChanged
{
    public event Action<float> OnSpeedChanged
    {
        add { this.moveSpeed.OnValueChanged += value; }
        remove { this.moveSpeed.OnValueChanged -= value; }
    }

    public float Speed
    {
        get { return this.moveSpeed.Value; }
    }

    [SerializeField]
    private FloatBehaviour moveSpeed;

    public void SetSpeed(float speed)
    {
        this.moveSpeed.Assign(speed);
    }
}