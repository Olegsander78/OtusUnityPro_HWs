using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public sealed class UnityEntityConditionGroup : UnityEntityCondition
    {
        [SerializeField]
        private Mode mode;

        [Space]
        [SerializeField]
        private UnityEntityCondition[] monoConditions;

        [SerializeField]
        private ScriptableEntityCondition[] scriptableConditions;

        private List<IEntityCondition> conditions;

        private void Awake()
        {
            this.conditions = new List<IEntityCondition>();
            this.conditions.AddRange(this.monoConditions);
            this.conditions.AddRange(this.scriptableConditions);
        }

        public override bool IsTrue(IEntity entity)
        {
            return this.mode switch
            {
                Mode.AND => this.All(entity),
                Mode.OR => this.Any(entity),
                _ => throw new Exception($"Mode is undefined {this.mode}")
            };
        }

        private bool All(IEntity entity)
        {
            for (int i = 0, count = this.conditions.Count; i < count; i++)
            {
                var condition = this.conditions[i];
                if (!condition.IsTrue(entity))
                {
                    return false;
                }
            }

            return true;
        }

        private bool Any(IEntity entity)
        {
            for (int i = 0, count = this.conditions.Count; i < count; i++)
            {
                var condition = this.conditions[i];
                if (condition.IsTrue(entity))
                {
                    return true;
                }
            }

            return false;
        }

        [Serializable]
        private enum Mode
        {
            AND,
            OR
        }
    }
}