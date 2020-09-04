using Generator;
using UnityEngine;

namespace Cells
{
    [CreateAssetMenu(menuName = MenuPath + "Enemy")]
    public class EnemyCellType : CellType
    {
        public override Cell CreateCell() => Generator.Cells.Trap;
    }
}