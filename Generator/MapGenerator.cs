using System;
using System.Collections.Generic;

namespace Generator
{
    public class MapGenerator
    {
        public float MinDensity { get; set; } = 0.25f;
        public float BranchProbability { get; set; } = 0.25f;
        
        private readonly Map _map;

        public MapGenerator(Map map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public IReadOnlyList<IRoom> RoomTemplates => _roomTemplates;

        public MapGenerator AddRoomTemplate(IRoom room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            if (room.Width != _map.RoomWidth || room.Height != _map.RoomHeight) throw new ArgumentException(nameof(room));
            
            _roomTemplates.Add(room);

            return this;
        }

        private readonly List<IRoom> _roomTemplates = new List<IRoom>();

        public MapGenerator RemoveRoomTemplate(IRoom room)
        {
            _roomTemplates.Remove(room);

            return this;
        }

        public void Generate()
        {
            _map.Clear();

            if (RoomTemplates.Count == 0) return;

            var (startX, startY) = GetRandomStart();
            var startingRoom = GetRandomRoomTemplate();

            _map.SetRoom(startX, startY, startingRoom);
            
            _queue.Clear();
            _queue.Enqueue((startX, startY));
            _roomCount = 1;

            while (_queue.Count > 0)
            {
                var (x, y) = _queue.Dequeue();
                
                TryBranchTo(x + 1, y);
                TryBranchTo(x - 1, y);
                TryBranchTo(x, y + 1);
                TryBranchTo(x, y - 1);
            }
            
            _map.AddOuterWalls();
        }

        private (int x, int y) GetRandomStart()
        {
            var x = _random.Next() % _map.Width;
            var y = _random.Next() % _map.Height;

            return (x, y);
        }

        private IRoom GetRandomRoomTemplate()
        {
            var index = _random.Next() % RoomTemplates.Count;
            return RoomTemplates[index];
        }

        private bool ShouldBranch => BelowMinDensity || _random.NextDouble() <= BranchProbability;

        private bool BelowMinDensity => (float) _roomCount / (_map.Width * _map.Height) < MinDensity;

        private void TryBranchTo(int x, int y)
        {
            if (!CanBranchTo(x, y) || !ShouldBranch) return;
            
            var room = GetRandomRoomTemplate();
            _map.SetRoom(x, y, room);
            
            _queue.Enqueue((x, y));
            _roomCount++;
        }

        private bool CanBranchTo(int x, int y)
        {
            if (x < 0 || x >= _map.Width) return false;
            if (y < 0 || y >= _map.Height) return false;

            return !_map.HasRoomAt(x, y);
        }

        private readonly Queue<(int x, int y)> _queue = new Queue<(int x, int y)>();
        private int _roomCount = 0;
        private readonly Random _random = new Random();
    }
}