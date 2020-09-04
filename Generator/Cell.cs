using System;

namespace Generator
{
	public readonly struct Cell : IEquatable<Cell>
	{
		public readonly char Symbol;

		public Cell(char symbol) => Symbol = symbol;

		public bool Equals(Cell other) => Symbol == other.Symbol;

		public override bool Equals(object obj)
		{
			if (!(obj is Cell other)) return false;

			return Equals(other);
		}

		public override int GetHashCode() => Symbol.GetHashCode();

		public static bool operator ==(Cell cell1, Cell cell2) => cell1.Equals(cell2);

		public static bool operator !=(Cell cell1, Cell cell2) => !(cell1 == cell2);

		public override string ToString() => Symbol.ToString();
	}
}