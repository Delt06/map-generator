using System;

namespace Generator
{
	public static class MapExtensions
	{
		public static Map AddOuterWalls(this Map map)
		{
			if (map == null) throw new ArgumentNullException(nameof(map));

			for (var x = 0; x < map.Width; x++)
			{
				for (var y = 0; y < map.Height; y++)
				{
					if (!map.TryGetRoomAt(x, y, out var room)) continue;

					AddOuterWallsWhereNeeded(map, room, x, y);
					AddCornerWallsWhereNeeded(map, room, x, y);
				}
			}

			return map;
		}

		public static bool TryGetRoomAt(this Map map, int x, int y, out IRoom room)
		{
			if (map == null) throw new ArgumentNullException(nameof(map));

			if (map.HasRoomAt(x, y))
			{
				room = map[x, y];
				return true;
			}

			room = default;
			return false;
		}

		private static void AddOuterWallsWhereNeeded(Map map, IRoom room, int x, int y)
		{
			if (map.HasNoRoomOrOutOfBounds(x - 1, y))
				room.AddLeftWall();

			if (map.HasNoRoomOrOutOfBounds(x + 1, y))
				room.AddRightWall();

			if (map.HasNoRoomOrOutOfBounds(x, y - 1))
				room.AddBottomWall();

			if (map.HasNoRoomOrOutOfBounds(x, y + 1))
				room.AddTopWall();
		}

		private static void AddCornerWallsWhereNeeded(Map map, IRoom room, int x, int y)
		{
			if (map.HasNoRoomOrOutOfBounds(x - 1, y - 1))
				room.AddWallAtLeftBottomCorner();

			if (map.HasNoRoomOrOutOfBounds(x - 1, y + 1))
				room.AddWallAtLeftTopCorner();

			if (map.HasNoRoomOrOutOfBounds(x + 1, y - 1))
				room.AddWallAtRightBottomCorner();

			if (map.HasNoRoomOrOutOfBounds(x + 1, y + 1))
				room.AddWallAtRightTopCorner();
		}

		private static bool HasNoRoomOrOutOfBounds(this Map map, int x, int y)
		{
			if (x < 0 || x >= map.Width) return true;
			if (y < 0 || y >= map.Height) return true;

			return !map.HasRoomAt(x, y);
		}

		private static void AddTopWall(this IRoom room) => room.AddHorizontalWallAt(room.Height - 1);

		private static void AddBottomWall(this IRoom room) => room.AddHorizontalWallAt(0);

		private static void AddHorizontalWallAt(this IRoom room, int y)
		{
			for (var x = 0; x < room.Width; x++)
			{
				room[x, y] = Cells.Wall;
			}
		}

		private static void AddLeftWall(this IRoom room) => room.AddVerticalWallAt(0);

		private static void AddRightWall(this IRoom room) => room.AddVerticalWallAt(room.Width - 1);

		private static void AddVerticalWallAt(this IRoom room, int x)
		{
			for (var y = 0; y < room.Height; y++)
			{
				room[x, y] = Cells.Wall;
			}
		}

		private static void AddWallAtLeftBottomCorner(this IRoom room)
		{
			room[0, 0] = Cells.Wall;
		}

		private static void AddWallAtRightBottomCorner(this IRoom room)
		{
			room[room.Width - 1, 0] = Cells.Wall;
		}

		private static void AddWallAtLeftTopCorner(this IRoom room)
		{
			room[0, room.Height - 1] = Cells.Wall;
		}

		private static void AddWallAtRightTopCorner(this IRoom room)
		{
			room[room.Width - 1, room.Height - 1] = Cells.Wall;
		}

		public static Map SetRoom(this Map map, int x, int y, IRoom room)
		{
			if (map == null) throw new ArgumentNullException(nameof(map));
			if (room == null) throw new ArgumentNullException(nameof(room));

			map.RemoveRoomAt(x, y);
			var destinationRoom = map.CreateRoomAt(x, y);
			room.CopyTo(destinationRoom);

			return map;
		}

		public static void CopyTo(this IRoom source, IRoom destination)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (destination == null) throw new ArgumentNullException(nameof(destination));

			if (source.Width != destination.Width)
				throw new ArgumentException(
					$"Source.Width differs from Destination.Width: {source.Width} and {destination.Width}.");
			if (source.Height != destination.Height)
				throw new ArgumentException(
					$"Source.Height differs from Destination.Height: {source.Height} and {destination.Height}.");

			for (var x = 0; x < source.Width; x++)
			{
				for (var y = 0; y < source.Height; y++)
				{
					destination[x, y] = source[x, y];
				}
			}
		}

		public static IRoom CreateRoomTemplate(this Map map)
		{
			if (map == null) throw new ArgumentNullException(nameof(map));

			return Room.OfSize(map.RoomWidth, map.RoomHeight);
		}
	}
}