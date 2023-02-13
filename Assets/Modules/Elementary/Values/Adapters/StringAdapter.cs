using System;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class StringAdapter : IValue<string>
    {
        public string Value
        {
            get { return this.id; }
        }

        [Space]
        [SerializeField]
        public Mode mode = Mode.CUSTOM;

        [ShowIf("mode", Mode.SCRIPTABLE_OBJECT)]
        [OptionalField]
        [SerializeField]
        public ScriptableString scriptableString;

        [ShowIf("mode", Mode.CUSTOM)]
        [SerializeField]
        public string id;

        [ShowIf("mode", Mode.GAME_OBJECT)]
        [OptionalField]
        [SerializeField]
        public GameObject targetObject;

        public string GetValue()
        {
            if (this.mode == Mode.SCRIPTABLE_OBJECT)
            {
                return this.scriptableString.Value;
            }

            if (this.mode == Mode.CUSTOM)
            {
                return this.id;
            }

            if (this.mode == Mode.GAME_OBJECT)
            {
                return this.targetObject.name;
            }

            throw new Exception($"Mode {this.mode} is undefined!");
        }

        public enum Mode
        {
            SCRIPTABLE_OBJECT = 0,
            CUSTOM = 1,
            GAME_OBJECT = 2
        }
    }
}