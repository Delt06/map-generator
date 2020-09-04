using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Turns
{
    public abstract class TurnMaker : MonoBehaviour, ITurnMaker
    {
        public virtual int Order => transform.GetSiblingIndex();
        public abstract bool CanMakeTurn { get; }
        public abstract void MakeTurn();
        
        public virtual void OnBeforeTurn() { }

        public int CompareTo([NotNull] ITurnMaker other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return Order.CompareTo(other.Order);
        }
    }
}