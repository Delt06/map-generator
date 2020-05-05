using Generator;
using UnityEngine;

namespace Cells
{
    [CreateAssetMenu(menuName = MenuPath + "Custom")]
    public sealed class CustomCellType : CellType
    {
        [SerializeField] private char _symbol = ' ';
    
        public override Cell CreateCell()
        {
            return new Cell(_symbol);
        }
    }
}