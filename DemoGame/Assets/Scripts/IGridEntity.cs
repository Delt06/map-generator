using System;
using UnityEngine;

public interface IGridEntity
{
    Vector3Int Position { get; set; }
    event EventHandler<PositionChangeArgs> PositionChanged;
}