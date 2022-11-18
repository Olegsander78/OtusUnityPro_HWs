using System;
using UnityEngine;

public class JumpConroller : MonoBehaviour
{
    [SerializeField]
    private EntityHW _unit;

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
