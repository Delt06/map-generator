using Sirenix.OdinInspector;
using UnityEngine;

namespace Turns
{
    [RequireComponent(typeof(GridControls))]
    [RequireComponent(typeof(IGridEntity))]
    public sealed class GridControlsTurnMaker : TurnMaker
    {
        [SerializeField, Required] private CollisionChecker _collisionChecker = default;

        public override bool CanMakeTurn => _gridControls.Motion != Vector3Int.zero &&
                                            _collisionChecker.IsFree(_entity.Position + _gridControls.Motion);

        public override void MakeTurn()
        {
            var newPosition = _entity.Position + _gridControls.Motion;
            _entity.Position = newPosition;
        }

        private void Awake()
        {
            _gridControls = GetComponent<GridControls>();
            _entity = GetComponent<IGridEntity>();
        }

        private GridControls _gridControls;
        private IGridEntity _entity;
    }
}