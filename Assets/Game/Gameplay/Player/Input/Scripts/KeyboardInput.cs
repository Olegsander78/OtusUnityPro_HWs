using System;
using UnityEngine;

public sealed class KeyboardInput : MonoBehaviour,
    IStartGameListener,
    IFinishGameListener
{
    public event Action<Vector3> OnMoveEvent;
    public event Action OnJumpEvent;
    public event Action OnRangeAttackEvent;
    public event Action OnMeleeAttackEvent;

    private void Awake()
    {
        enabled = false;
    }

    private void Update()
    {
        HandleKeyboard();
    }

    void IStartGameListener.OnStartGame()
    {
        enabled = true;
    }

    void IFinishGameListener.OnFinishGame()
    {
        enabled = false;
    }

    private void HandleKeyboard()
    {

        Move(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(1))
            RangeAttack();

        if (Input.GetMouseButtonDown(0))
            MeleeAttack();
    }

    private void Move(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    private void Jump()
    {
        OnJumpEvent?.Invoke();
    }

    private void RangeAttack()
    {
        OnRangeAttackEvent?.Invoke();
    }

    private void MeleeAttack()
    {
        OnMeleeAttackEvent?.Invoke();
    }
}



//if (Input.GetKey(KeyCode.UpArrow))
//{
//    Move(Vector3.forward);
//}
//else if (Input.GetKey(KeyCode.DownArrow))
//{
//    Move(Vector3.back);
//}
//else if (Input.GetKey(KeyCode.LeftArrow))
//{
//    Move(Vector3.left);
//}
//else if (Input.GetKey(KeyCode.RightArrow))
//{
//    Move(Vector3.right);
//}