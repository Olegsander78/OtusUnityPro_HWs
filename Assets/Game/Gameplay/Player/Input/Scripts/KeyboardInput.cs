using System;
using UnityEngine;
using GameElements;

public sealed class KeyboardInput : MonoBehaviour,
    IGameStartElement,
    IGameFinishElement
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

    void IGameStartElement.StartGame(IGameContext context)
    {
        enabled = true;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        enabled = false;
    }

    private void HandleKeyboard()
    {

        //Move(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(2))
            RangeAttack();

        if (Input.GetMouseButtonDown(1))
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
