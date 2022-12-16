using UnityEngine;


[CreateAssetMenu(
    fileName = "Object Type",
    menuName = "GameEngine/New Scriptable Object Type"
)]
public sealed class ScriptableObjectType : ScriptableObject
{
    public ObjectType ObjectType
    {
        get { return this.objectType; }
    }

    [SerializeField]
    private ObjectType objectType;
}