using UnityEngine;


    public interface IComponent_GetPosition
    {
        Vector3 Position { get; }
    }

    public interface IComponent_SetPosition
    {
        void SetPosition(Vector3 position);
    }

    public interface IComponent_GetRotation
    {
        Quaternion Rotation { get; }
    }

    public interface IComponent_SetRotation
    {
        void SetRotation(Quaternion rotation);
    }

public interface IComponent_LookAtPosition
{
    void LookAtPosition(Vector3 position);
}