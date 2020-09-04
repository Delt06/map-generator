using System;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Turns
{
    [RequireComponent(typeof(IGridEntity))]
    public sealed class EnemyTurnMaker : TurnMaker
    {
        public CollisionChecker CollisionChecker;

        public override bool CanMakeTurn => true;

        public override void MakeTurn()
        {
            Assert.IsNotNull(CollisionChecker);
            
            Vector3Int direction;

            switch (Random.Range(0, 4))
            {
                case 0: direction = Vector3Int.right; break;
                case 1: direction = Vector3Int.left; break;
                case 2: direction = Vector3Int.up; break;
                case 3: direction = Vector3Int.down; break;
                default: direction = Vector3Int.zero; break;
            }

            var newPosition = _gridEntity.Position + direction;
            if (CollisionChecker.IsFree(newPosition))
                _gridEntity.Position = newPosition;
        }

        private void Awake()
        {
            _gridEntity = GetComponent<IGridEntity>();
            Assert.IsNotNull(_gridEntity);
        }

        private IGridEntity _gridEntity;
    }
}