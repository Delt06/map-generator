using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class Character : MonoBehaviour
{
    [SerializeField, Required] private Tilemap _tilemap = default;
    [SerializeField, Required] private TileBase _tile = default;
    [SerializeField] private Vector3Int _position = default;

    public Vector3Int Position
    {
        get => _position;
        set
        {
            _tilemap.SetTile(_position, null);
            _position = value;
            _tilemap.SetTile(_position, _tile);
        }
    }

    private void Awake()
    {
        Position = Position;
    }
}