using System;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class StringAdapter
    {
        public string Value
        {
            get { return this.id; }
        }

        [Space]
        [SerializeField]
        private Mode mode = Mode.CUSTOM;

        [ShowIf("mode", Mode.SCRIPTABLE_OBJECT)]
        [OptionalField]
        [SerializeField]
        private ScriptableString scriptableString;

        [ShowIf("mode", Mode.CUSTOM)]
        [SerializeField]
        private string id;

        [ShowIf("mode", Mode.GAME_OBJECT)]
        [OptionalField]
        [SerializeField]
        private GameObject targetObject;

        private string GetId()
        {
            if (this.mode == Mode.SCRIPTABLE_OBJECT)
            {
                return this.scriptableString.value;
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

        private enum Mode
        {
            SCRIPTABLE_OBJECT = 0,
            CUSTOM = 1,
            GAME_OBJECT = 2
        }
    }
}