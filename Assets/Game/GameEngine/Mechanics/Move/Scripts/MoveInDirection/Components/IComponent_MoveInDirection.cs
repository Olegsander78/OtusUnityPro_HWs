using System;
using UnityEngine;

public interface IComponent_MoveInDirection
{
    event Action<float> OnSpeedChanged;
    float Speed { get; }
    void Move(Vector3 vector);
    void SetSpeed(float speed);
}
