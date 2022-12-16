using System;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class ObjectTypeAdapter
{
    public ObjectType Value
    {
        get { return this.GetValue(); }
    }

    [SerializeField]
    private Mode mode = Mode.BASE;

    [Space]
    [ShowIf("mode", Mode.BASE)]
    [SerializeField]
    private ObjectType baseObjectType;

    [Space]
    [ShowIf("mode", Mode.SCRIPTABLE_OBJECT)]
    [OptionalField]
    [SerializeField]
    private ScriptableObjectType scriptableObjectType;

    private ObjectType GetValue()
    {
        if (this.mode == Mode.BASE)
        {
            return this.baseObjectType;
        }

        if (this.mode == Mode.SCRIPTABLE_OBJECT)
        {
            return this.scriptableObjectType.ObjectType;
        }

        throw new Exception($"Mode {this.mode} is undefined!");
    }

    private enum Mode
    {
        BASE = 0,
        SCRIPTABLE_OBJECT = 1
    }
}