using Entities;
using System;
using UnityEngine;

[AddComponentMenu("Gameplay/Hero/Hero Jump Controller")]
public class JumpConroller : MonoBehaviour
{
    [SerializeField]
    private UnityEntityBase _unit;

    private IComponent_Jump _jumpComponent;

    private void Awake()
    {
        _jumpComponent = _unit.Get<IComponent_Jump>();
    }
    private void Update()
    {
        HandleKeyboard();
    }

    private void HandleKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    protected void Jump()
    {
        _jumpComponent.Jump();
    }
}
