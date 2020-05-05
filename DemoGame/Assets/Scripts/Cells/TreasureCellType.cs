using Generator;
using UnityEngine;

namespace Cells
{
    [CreateAssetMenu(menuName = MenuPath + "Treasure")]
    public sealed class TreasureCellType : CellType
    {
        public override Cell CreateCell() => Generator.Cells.Treasure;
    }
}