using UnityEngine;
using Entities;

public abstract class AbstractMoveController : MonoBehaviour
{
    private void FixedUpdate()
    {
        HandleKeyboard();
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

    protected abstract void Move(Vector3 direction);
}
