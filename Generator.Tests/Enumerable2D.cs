using System.Collections.Generic;

namespace Generator.Tests
{
	public static class Enumerable2D
	{
		public static IEnumerable<(int x, int y)> Range(int countX, int countY)
		{
			for (var x = 0; x < countX; x++)
			{
				for (var y = 0; y < countY; y++)
				{
					yield return (x, y);
				}
			}
		}
	}
}