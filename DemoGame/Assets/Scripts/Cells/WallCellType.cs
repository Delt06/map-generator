using Generator;
using UnityEngine;

namespace Cells
{
    [CreateAssetMenu(menuName = MenuPath + "Wall")]
    public class WallCellType : CellType
    {
        public override Cell CreateCell() => Generator.Cells.Wall;
    }
}