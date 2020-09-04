using System;
using UnityEngine;

[DisallowMultipleComponent]
public class GridEntity : MonoBehaviour, IGridEntity
{
    [SerializeField] private Vector3Int _position = default;

    public Vector3Int Position
    {
        get => _position;
        set
        {
            var oldPosition = _position;
            _position = value;
            OnPositionChanged(oldPosition, _position);
        }
    }

    protected virtual void OnPositionChanged(Vector3Int oldPosition, Vector3Int newPosition)
    {
        var args = new PositionChangeArgs(oldPosition, newPosition);
        PositionChanged?.Invoke(this, args);
    }

    public event EventHandler<PositionChangeArgs> PositionChanged;
}