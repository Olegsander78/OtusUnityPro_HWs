using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Agents
{
    public abstract class Agent_MoveByPath<T> : AgentCoroutine
    {
        private const int POINTS_BUFFER = 16;

        public event Action OnPathFinished;

        public bool IsPathFinished
        {
            get { return this.isPathFinished; }
        }

        private readonly List<T> currentPath;

        private int pointer;

        private bool isPathFinished;

        public Agent_MoveByPath(
            MonoBehaviour coroutineDispatcher,
            YieldInstruction framePeriod = null
        ) : base(coroutineDispatcher, framePeriod)
        {
            this.currentPath = new List<T>(POINTS_BUFFER);
        }

        public void SetPath(IEnumerable<T> points)
        {
            this.currentPath.Clear();
            this.currentPath.AddRange(points);

            this.pointer = 0;
            this.isPathFinished = false;
        }

        protected override void Update()
        {
            if (this.isPathFinished)
            {
                return;
            }

            if (this.pointer >= this.currentPath.Count)
            {
                this.isPathFinished = true;
                this.OnPathFinished?.Invoke();
                return;
            }

            var targetPoint = this.currentPath[this.pointer];
            if (this.CheckPointReached(targetPoint))
            {
                this.pointer++;
            }
            else
            {
                this.MoveToPoint(targetPoint);
            }
        }

        protected abstract bool CheckPointReached(T point);

        protected abstract void MoveToPoint(T target);
    }
}