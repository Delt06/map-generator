using System;
using Generator;
using JetBrains.Annotations;
using UnityEngine;

public static class MapExtensions
{
        public static Map GetCroppedClone([NotNull] this Map map)
        {
                if (map == null) throw new ArgumentNullException(nameof(map));
                if (!map.HasBounds(out var bounds)) throw new InvalidOperationException("The map is empty.");

                var (minX, maxX, minY, maxY) = bounds;

                var width = maxX - minX + 1;
                var height = maxY - minY + 1;

                var clone = new Map.Builder()
                        .WithSize(width, height)
                        .WithRoomSize(map.RoomWidth, map.RoomHeight)
                        .Build();

                for (var x = 0; x < width; x++)
                {
                        for (var y = 0; y < height; y++)
                        {
                                var sourceX = minX + x;
                                var sourceY = minY + y;
                                
                                if (!map.TryGetRoomAt(sourceX, sourceY, out var sourceRoom)) continue;

                                var destinationRoom = clone.CreateRoomAt(x, y);
                                sourceRoom.CopyTo(destinationRoom);
                        }
                }

                return clone;
        }

        private static bool HasBounds(this Map map, out (int minX, int maxX, int minY, int maxY) bounds)
        {
                bounds = (map.Width, -1, map.Height, -1);

                for (var x = 0; x < map.Width; x++)
                {
                        for (var y = 0; y < map.Height; y++)
                        {
                                if (!map.HasRoomAt(x, y)) continue;

                                bounds.minX = Mathf.Min(bounds.minX, x);
                                bounds.minY = Mathf.Min(bounds.minY, y);
                                
                                bounds.maxX = Mathf.Max(bounds.maxX, x);
                                bounds.maxY = Mathf.Max(bounds.maxY, y);
                        }
                }

                return bounds.maxX >= bounds.minX && bounds.maxY >= bounds.minY;
        }

        public static Map GetCloneWithExtraBorderRooms([NotNull] this Map map, int borderThicknessInRooms)
        {
                if (map == null) throw new ArgumentNullException(nameof(map));
                if (borderThicknessInRooms < 0) throw new ArgumentOutOfRangeException(nameof(borderThicknessInRooms));
                if (borderThicknessInRooms == 0) return map.Clone();

                var newWidth = map.Width + borderThicknessInRooms * 2;
                var newHeight = map.Height + borderThicknessInRooms * 2;

                var newMap = new Map.Builder()
                        .WithSize(newWidth, newHeight)
                        .WithRoomSize(map.RoomWidth, map.RoomHeight)
                        .Build();

                for (var x = 0; x < newWidth; x++)
                {
                        for (var y = 0; y < newHeight; y++)
                        {
                                var sourceX = x - borderThicknessInRooms;
                                var sourceY = y - borderThicknessInRooms;
                                
                                if (OutOfBounds(map, sourceX, sourceY)) continue;
                                if (!map.TryGetRoomAt(sourceX, sourceY, out var sourceRoom)) continue;
                                
                                var destinationRoom = newMap.CreateRoomAt(x, y);
                                sourceRoom.CopyTo(destinationRoom);
                        }
                }

                return newMap;
        }

        private static bool OutOfBounds(Map map, int x, int y)
        {
                return x < 0 || x >= map.Width ||
                       y < 0 || y >= map.Height;
        }

        public static Map CreateRemainingRoomsAndFillWithWalls([NotNull] this Map map)
        {
                if (map == null) throw new ArgumentNullException(nameof(map));
                
                for (var x = 0; x < map.Width; x++)
                {
                        for (var y = 0; y < map.Height; y++)
                        {
                                if (map.HasRoomAt(x, y)) continue;

                                map.CreateRoomAt(x, y).FillHolesWithWalls();
                        }
                }

                return map;
        }

        public static IRoom FillHolesWithWalls([NotNull] this IRoom room)
        {
                if (room == null) throw new ArgumentNullException(nameof(room));

                for (var x = 0; x < room.Width; x++)
                {
                        for (var y = 0; y < room.Height; y++)
                        {
                                if (room[x, y] != Generator.Cells.Empty) continue;
                                
                                room[x, y] = Generator.Cells.Wall;
                        }
                }

                return room;
        }
}