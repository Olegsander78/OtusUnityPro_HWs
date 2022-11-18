using UnityEngine;
using Elementary;

public class Component_Jump : MonoBehaviour, IComponent_Jump
{
    [SerializeField]
    private EventReceiver _jumpReceiver;

    public void Jump()
    {
        _jumpReceiver.Call();
    }    
}
