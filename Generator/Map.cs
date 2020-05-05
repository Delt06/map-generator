using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Generator
{
    public sealed class Map : IEnumerable<IRoom>, ICloneable
    {
        private readonly IRoom[,] _rooms;
        
        public int Width { get; }
        public int Height { get; }
        public int RoomWidth { get; }
        public int RoomHeight { get; }

        public IRoom CreateRoomAt(int x, int y)
        {
            CheckThatCoordinatesAreValid(x, y);

            var room = CreateRoom();
            _rooms[x, y] = room;

            return room;
        }

        private IRoom CreateRoom()
        {
            return Room.OfSize(RoomWidth, RoomHeight);
        }

        public void RemoveRoomAt(int x, int y)
        {
            CheckThatCoordinatesAreValid(x, y);

            _rooms[x, y] = null;
        }

        public bool HasRoomAt(int x, int y)
        {
            CheckThatCoordinatesAreValid(x, y);

            return _rooms[x, y] != null;
        }

        private void CheckThatCoordinatesAreValid(int x, int y)
        {
            if (x < 0 || x >= Width) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y >= Height) throw new ArgumentOutOfRangeException(nameof(y));
        }
        
        public IRoom this[int x, int y]
        {
            get
            {
                CheckThatCoordinatesAreValid(x, y);

                return _rooms[x, y] ?? throw new ArgumentException($"Room at ({x.ToString()}; {y.ToString()}) does not exist.");
            }
        }

        public int TotalWidth => Width * RoomWidth;
        public int TotalHeight => Height * RoomHeight;

        public IEnumerator<IRoom> GetEnumerator()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (HasRoomAt(x, y))
                        yield return _rooms[x, y];
                }
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            for (var y = TotalHeight - 1; y >= 0; y--)
            {
                for (var x = 0; x < TotalWidth; x++)
                {
                    var roomX = x / RoomWidth;
                    var roomY = y / RoomHeight;

                    var room = _rooms[roomX, roomY];
                    
                    if (room == null)
                    {
                        stringBuilder.Append(' ');
                    }
                    else
                    {
                        var cellX = x % RoomWidth;
                        var cellY = y % RoomHeight;

                        stringBuilder.Append(room[cellX, cellY].Symbol);
                    }
                }

                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }

        object ICloneable.Clone() => Clone();

        public Map Clone()
        {
            var clone = new Map(Width, Height, RoomWidth, RoomHeight);

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (!HasRoomAt(x, y)) continue;

                    var sourceRoom = _rooms[x, y];
                    var destinationRoom = clone.CreateRoomAt(x, y);
                    
                    Copy(sourceRoom, destinationRoom);
                }
            }

            return clone;
        }

        private static void Copy(IRoom source, IRoom destination)
        {
            for (var x = 0; x < source.Width; x++)
            {
                for (var y = 0; y < source.Height; y++)
                {
                    destination[x, y] = source[x, y];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Map(int width, int height, int roomWidth, int roomHeight)
        {
            Width = width;
            Height = height;
            RoomWidth = roomWidth;
            RoomHeight = roomHeight;
            
            _rooms = new IRoom[Width, Height];
        }

        public class Builder
        {
            public int Width { get; private set; } = 1;
            public int Height { get; private set; } = 1;
            public int RoomWidth { get; private set; } = 1;
            public int RoomHeight { get; private set; } = 1;

            public Builder WithSize(int width, int height)
            {
                if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
                if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

                Width = width;
                Height = height;

                return this;
            }

            public Builder WithRoomSize(int width, int height)
            {
                if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
                if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

                RoomWidth = width;
                RoomHeight = height;

                return this;
            }

            public Map Build()
            {
                return new Map
                (
                    Width, Height, 
                    RoomWidth, RoomHeight
                );
            }
        }

        public void Clear()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    _rooms[x, y] = null;
                }
            }
        }
    }
}