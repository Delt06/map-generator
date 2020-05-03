using System;

namespace Generator
{
    public readonly struct Cell : IEquatable<Cell>
    {
        public readonly char Symbol;

        public Cell(char symbol)
        {
            Symbol = symbol;
        }

        public bool Equals(Cell other)
        {
            return Symbol == other.Symbol;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Cell other)) return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        public static bool operator ==(Cell cell1, Cell cell2)
        {
            return cell1.Equals(cell2);
        }

        public static bool operator !=(Cell cell1, Cell cell2)
        {
            return !(cell1 == cell2);
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }
    }
}