using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Iterators
{
    public abstract class Iterator<T> : IEnumerator<T>
    {
        public T Current
        {
            get { return this.movePoints[this.pointer]; }
        }

        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        protected T[] movePoints;

        protected int pointer;

        public Iterator(T[] movePoints)
        {
            this.SetPoints(movePoints);
        }

        public Iterator()
        {
            this.movePoints = new T[0];
        }

        public void SetPoints(T[] movePoints)
        {
            this.movePoints = movePoints;
            this.pointer = 0;
        }

        public abstract bool MoveNext();

        public abstract void Reset();

        public abstract void Dispose();
    }
}