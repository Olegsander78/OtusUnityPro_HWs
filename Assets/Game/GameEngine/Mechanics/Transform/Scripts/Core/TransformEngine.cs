using Sirenix.OdinInspector;
using UnityEngine;


public sealed class TransformEngine : MonoBehaviour
{
    private static readonly Vector3 UP = Vector3.up;

    [PropertySpace]
    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    public Vector3 WorldPosition
    {
        get { return this.GetWorldPosition(); }
    }

    [PropertyOrder(-9)]
    [ReadOnly]
    [ShowInInspector]
    public Quaternion WorldRotation
    {
        get { return this.GetWorldRotation(); }
    }

    [Space]
    [SerializeField]
    private Transform sourcePositionTransform;

    [SerializeField]
    private Transform[] movingTransforms;

    [Space]
    [SerializeField]
    private Transform sourceRotationTransform;

    [SerializeField]
    private Transform[] rotatingTransforms;

    [Title("Methods")]
    [Button]
    [GUIColor(0, 1, 0)]
    public void SetPosiiton(Vector3 position)
    {
        for (int i = 0, count = this.movingTransforms.Length; i < count; i++)
        {
            var transform = this.movingTransforms[i];
            transform.position = position;
        }
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void MovePosition(Vector3 vector)
    {
        var newPosition = this.sourcePositionTransform.position + vector;
        this.SetPosiiton(newPosition);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public bool IsDistanceReached(Vector3 targetPosition, float minDistance)
    {
        var distanceVector = this.sourcePositionTransform.position - targetPosition;
        distanceVector.y = 0.0f;
        return distanceVector.sqrMagnitude <= minDistance * minDistance;
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void SetRotation(Quaternion rotation)
    {
        for (int i = 0, count = this.rotatingTransforms.Length; i < count; i++)
        {
            var transform = this.rotatingTransforms[i];
            transform.rotation = rotation;
        }
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void LookAtPosition(Vector3 targetPosition)
    {
        var distanceVector = targetPosition - this.sourcePositionTransform.position;
        var direction = distanceVector.normalized;
        this.LookInDirection(direction);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void LookInDirection(Vector3 direction)
    {
        var newRotation = Quaternion.LookRotation(direction, UP);
        this.SetRotation(newRotation);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void RotateTowardsAtPosition(Vector3 targetPosition, float speed, float deltaTime)
    {
        var distanceVector = targetPosition - this.sourcePositionTransform.position;
        var direction = distanceVector.normalized;
        this.RotateTowardsInDirection(direction, speed, deltaTime);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void RotateTowardsInDirection(Vector3 direction, float speed, float deltaTime)
    {
        var currentRotation = this.sourceRotationTransform.rotation;
        var targetRotation = Quaternion.LookRotation(direction, UP);
        var newRotation = Quaternion.Slerp(currentRotation, targetRotation, speed * deltaTime);
        this.SetRotation(newRotation);
    }

    private Vector3 GetWorldPosition()
    {
        if (!ReferenceEquals(this.sourcePositionTransform, null))
        {
            return this.sourcePositionTransform.position;
        }

        return Vector3.zero;
    }

    private Quaternion GetWorldRotation()
    {
        if (!ReferenceEquals(this.sourceRotationTransform, null))
        {
            return this.sourceRotationTransform.rotation;
        }

        return Quaternion.identity;
    }
}