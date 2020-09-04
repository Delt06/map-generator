using System;

namespace Generator
{
	public static class RoomExtensions
	{
		public static IRoom SetCell(this IRoom room, int x, int y, Cell cell)
		{
			if (room == null) throw new ArgumentNullException(nameof(room));

			room[x, y] = cell;

			return room;
		}

		public static IRoom RemoveCell(this IRoom room, int x, int y)
		{
			if (room == null) throw new ArgumentNullException(nameof(room));

			room[x, y] = Cells.Empty;

			return room;
		}
	}
}