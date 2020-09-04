using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public sealed class TileDrawer : MonoBehaviour
{
    public void Draw(Vector3Int position, TileBase tile)
    {
        _tilemap.SetTile(position, tile);
    }

    public void Clear(Vector3Int position)
    {
        _tilemap.SetTile(position, null);
    }
    
    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        Assert.IsNotNull(_tilemap);
    }

    private Tilemap _tilemap;
}