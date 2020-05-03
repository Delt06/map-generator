using System;
using System.Text;

namespace Generator
{
    public sealed class Room : IRoom
    {
        private readonly Cell[,] _cells;
        
        public int Width { get; }
        public int Height { get; }

        private Room(int width, int height)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            
            Width = width;
            Height = height;
            
            _cells = new Cell[Width, Height];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    _cells[x, y] = Cells.Empty;
                }
            }
        }

        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width) throw new ArgumentOutOfRangeException(nameof(x));
                if (y < 0 || y >= Height) throw new ArgumentOutOfRangeException(nameof(x));

                return _cells[x, y];
            }
            
            set
            {
                if (x < 0 || x >= Width) throw new ArgumentOutOfRangeException(nameof(x));
                if (y < 0 || y >= Width) throw new ArgumentOutOfRangeException(nameof(x));

                _cells[x, y] = value;
            }
        }

        public bool Equals(IRoom other)
        {
            if (other == null) return false;
            if (Width != other.Width) return false;
            if (Height != other.Height) return false;

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    if (this[x, y] != other[x, y])
                        return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IRoom other)) return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 0;

                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        hashCode = _cells[x, y].GetHashCode() + 23 * x + 23 * 23 * y;
                    }
                }

                return hashCode;
            }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            
            for (var y = Height - 1; y >= 0; y--)
            {
                for (var x = 0; x < Width; x++)
                {
                    var cell = this[x, y];
                    stringBuilder.Append(cell.Symbol);
                }
                
                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }

        public IRoom Clone()
        {
            var clone = new Room(Width, Height);
            
            Array.Copy(_cells, clone._cells, Width * Height);

            return clone;
        }

        object ICloneable.Clone() => Clone();

        public static IRoom OfSize(int width, int height)
        {
            return new Room(width, height);
        }
    }
}