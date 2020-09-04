using NUnit.Framework;

namespace Generator.Tests
{
	[TestFixture]
	public class CellTests
	{
		[Test]
		public void Equals_ToCellWithSameSymbol_ReturnsTrue()
		{
			var cell1 = new Cell('a');
			var cell2 = new Cell('a');

			Assert.That(Equals(cell1, cell2));
		}

		[Test]
		public void Equals_ToDifferentType_ReturnsFalse()
		{
			Assert.That(Equals(Cells.Wall, new object()), Is.False);
		}

		[Test]
		public void ToString_ReturnsSymbol()
		{
			var cell = new Cell('a');
			Assert.That(cell.ToString(), Is.EqualTo(cell.Symbol.ToString()));
		}
	}
}