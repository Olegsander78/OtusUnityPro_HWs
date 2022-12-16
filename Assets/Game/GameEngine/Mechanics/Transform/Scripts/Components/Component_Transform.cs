using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Transform»")]
public sealed class Component_Transform : MonoBehaviour,
    IComponent_GetPosition,
    IComponent_SetPosition,
    IComponent_GetRotation,
    IComponent_SetRotation
{
    public Vector3 Position
    {
        get { return this.root.position; }
    }

    public Quaternion Rotation
    {
        get { return this.root.rotation; }
    }

    [SerializeField]
    private Transform root;

    public void SetPosition(Vector3 position)
    {
        this.root.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        this.root.rotation = rotation;
    }
}