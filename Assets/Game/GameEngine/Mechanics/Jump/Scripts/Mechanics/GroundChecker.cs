using UnityEngine;
using Elementary;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private float _groundPointRadius;

    [SerializeField]
    private Transform _groundPoint;

    [SerializeField]
    private LayerMask _whatIsGround;
    public bool IsGrounded()
    {
        return Physics.CheckSphere(_groundPoint.position, _groundPointRadius, _whatIsGround);
    }
}
