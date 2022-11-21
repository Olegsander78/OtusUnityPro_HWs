using System;
using UnityEngine;
using GameElements;

public sealed class KeyboardInput : MonoBehaviour,
    IStartGameListener,
    IFinishGameListener
{
    public event Action<Vector3> OnMove;

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
    }

    private void Move(Vector3 direction)
    {
        OnMove?.Invoke(direction);
    }
}
