//NO BUSSINESS LOGIC
using UnityEngine;
using Elementary;

public class Component_MoveInDirection : MonoBehaviour, IComponent_MoveInDirection
{
    [SerializeField]
    private EventReceiver_Vector3 _moveReceiver;
    public void Move(Vector3 direction)
    {
        _moveReceiver.Call(direction);
    }
}
