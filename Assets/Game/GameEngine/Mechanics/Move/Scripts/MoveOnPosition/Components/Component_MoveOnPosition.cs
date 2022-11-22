using UnityEngine;
using Elementary;

public class Component_MoveOnPosition : MonoBehaviour, IComponent_MoveOnPosition
{
    [SerializeField]
    private EventReceiver_Vector3 _moveReceiver;
    public void Move(Vector3 position)
    {
        _moveReceiver.Call(position);
    }
}
