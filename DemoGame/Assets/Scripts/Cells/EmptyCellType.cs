using Generator;
using UnityEngine;

namespace Cells
{
    [CreateAssetMenu(menuName = MenuPath + "Empty")]
    public sealed class EmptyCellType : CellType
    {
        public override Cell CreateCell() => Generator.Cells.Empty;
    }
}