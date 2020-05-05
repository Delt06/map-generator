using Generator;
using UnityEngine;

namespace Cells
{
    [CreateAssetMenu(menuName = MenuPath + "Trap")]
    public class TrapCellType : CellType
    {
        public override Cell CreateCell() => Generator.Cells.Trap;
    }
}