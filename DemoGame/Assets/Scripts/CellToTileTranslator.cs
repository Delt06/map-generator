using System;
using Cells;
using Generator;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class CellToTileTranslator : ScriptableObject
{
    [SerializeField, TableList(AlwaysExpanded = true)]
    private Translation[] _translations = default;
    
    [SerializeField, Required, AssetSelector]
    private TileBase _fallbackTile = default;

    public TileBase Translate(Cell cell)
    {
        foreach (var translation in _translations)
        {
            if (translation.Tile == null) continue;

            if (cell == translation.Cell.CreateCell())
                return translation.Tile;
        }

        return _fallbackTile;
    }

    [Serializable]
    private struct Translation
    {
        #pragma warning disable 0649

        [Required, AssetSelector] public CellType Cell;
        [Required, AssetSelector] public TileBase Tile;

#pragma warning restore 0649
    }
}