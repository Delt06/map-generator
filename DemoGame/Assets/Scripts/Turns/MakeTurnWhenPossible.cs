using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace Turns
{
    [RequireComponent(typeof(ITurnMaker))]
    public sealed class MakeTurnWhenPossible : MonoBehaviour
    {
        [SerializeField, Required] private TurnSystem _turnSystem = default;

        private void Update()
        {
            if (_turnMaker.CanMakeTurn)
                _turnSystem.MakeTurn();
        }

        private void Awake()
        {
            _turnMaker = GetComponent<ITurnMaker>();
            Assert.IsNotNull(_turnMaker);
        }

        private ITurnMaker _turnMaker;
    }
}