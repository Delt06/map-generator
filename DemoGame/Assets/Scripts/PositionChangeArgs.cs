using UnityEngine;

public readonly struct PositionChangeArgs
{
    public readonly Vector3Int Old;
    public readonly Vector3Int New;

    public PositionChangeArgs(Vector3Int old, Vector3Int @new)
    {
        Old = old;
        New = @new;
    }
}