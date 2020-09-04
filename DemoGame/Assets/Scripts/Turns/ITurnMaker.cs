using System;

namespace Turns
{
    public interface ITurnMaker : IComparable<ITurnMaker>
    {
        int Order { get; }

        bool CanMakeTurn { get; }
        
        void MakeTurn();
        void OnBeforeTurn();
    }
}