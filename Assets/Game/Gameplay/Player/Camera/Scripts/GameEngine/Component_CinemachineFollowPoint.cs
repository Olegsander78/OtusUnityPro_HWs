using UnityEngine;

public class Component_CinemachineFollowPoint : MonoBehaviour
{
    public Transform Point => _point;
    
    [SerializeField]
    private Transform _point;

}
