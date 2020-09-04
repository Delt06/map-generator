using System.Collections.Generic;
using System.Linq;
using DataStructures;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Turns
{
        public sealed class TurnSystem : TurnMaker
        {
                public override bool CanMakeTurn => true;

                public override void MakeTurn()
                {
                        foreach (var turnMaker in _turnMakers)
                        {
                                if (turnMaker.CanMakeTurn)
                                        turnMaker.MakeTurn();
                        }
                        
                        OnBeforeTurn();
                }

                public override void OnBeforeTurn()
                {
                        foreach (var turnMaker in _turnMakers)
                        {
                                turnMaker.OnBeforeTurn();
                        }
                }

                public void Add([NotNull] ITurnMaker turnMaker)
                {
                        Assert.IsNotNull(turnMaker);
                        _turnMakers.Add(turnMaker);
                }

                private void Start()
                {
                        OnBeforeTurn();
                }

                private void Awake()
                {
                        foreach (var turnMaker in ChildrenTurnMakers)
                        {
                                _turnMakers.Add(turnMaker);
                        }
                }

                private IEnumerable<ITurnMaker> ChildrenTurnMakers => Children
                        .Select(c => c.GetComponent<ITurnMaker>())
                        .Where(turnMaker => turnMaker != null);

                private IEnumerable<Transform> Children => transform.Cast<Transform>();

                private readonly MinHeap<ITurnMaker> _turnMakers = new MinHeap<ITurnMaker>();
        }
}