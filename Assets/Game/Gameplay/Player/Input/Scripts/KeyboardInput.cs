using System;
using UnityEngine;
using GameElements;

public sealed class KeyboardInput : MonoBehaviour,
    IStartGameListener,
    IFinishGameListener
{
    public event Action<Vector3> OnMoveEvent;
    public event Action OnJumpEvent;

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
    }

    private void Move(Vector3 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    private void Jump()
    {
        OnJumpEvent?.Invoke();
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