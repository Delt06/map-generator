using System;
using Cells;
using Generator;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class ColorToCellTranslator : ScriptableObject
{
    [SerializeField, TableList(AlwaysExpanded = true), AssetSelector] 
    private Translation[] _translations = default;

    [SerializeField, Required, AssetSelector]
    private CellType _fallbackCell = default;

    [SerializeField] private bool _ignoreTransparency = true;

    public Cell Translate(Color color)
    {
        foreach (var translation in _translations)
        {
            if (translation.Cell == null) continue;

            if (Equal(translation.Color, color)) 
                return translation.Cell.CreateCell();
        }

        return _fallbackCell.CreateCell();
    }

    private bool Equal(Color color1, Color color2)
    {
        if (_ignoreTransparency)
            color2.a = color1.a;

        return color1 == color2;
    }

    [Serializable]
    private struct Translation
    {
        #pragma warning disable 0649
        
        public Color Color;

        [Required] 
        public CellType Cell;
        
        #pragma warning restore 0649
    }
}