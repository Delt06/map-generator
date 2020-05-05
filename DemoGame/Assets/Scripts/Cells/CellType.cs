using Generator;
using UnityEngine;

namespace Cells
{
    public abstract class CellType : ScriptableObject
    {
        public abstract Cell CreateCell();

        protected const string MenuPath = "Cell Type/";
    }
}