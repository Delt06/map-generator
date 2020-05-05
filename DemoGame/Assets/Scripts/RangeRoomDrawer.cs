using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(TileMapGenerator))]
public class RangeRoomDrawer : MonoBehaviour
{
        [SerializeField, Required] private Transform _observer = default;
        [SerializeField] private int _extent = 2;

        private void FixedUpdate()
        {
                var observerPosition = _generator.WorldToCell(_observer.position);

                var offset = Vector2Int.one * _extent;
                var min = observerPosition - offset;
                var max = observerPosition + offset;

                var minRoom = ToRoom(min);
                var maxRoom = ToRoom(max);
                
                var room = new Vector2Int();
                
                _newlyDrawnRooms.Clear();

                for (room.x = minRoom.x; room.x <= maxRoom.x; room.x++)
                {
                        for (room.y = minRoom.y; room.y <= maxRoom.y; room.y++)
                        {
                                if (OutOfBounds(new Vector2Int(room.x, room.y))) continue;

                                _newlyDrawnRooms.Add(room);
                        }
                }

                foreach (var newlyDrawnRoom in _newlyDrawnRooms)
                {
                        if (!_drawnRooms.Contains(newlyDrawnRoom)) 
                                _generator.DrawRoom(newlyDrawnRoom.x, newlyDrawnRoom.y);
                }

                foreach (var drawnRoom in _drawnRooms)
                {
                        if (!_newlyDrawnRooms.Contains(drawnRoom))
                                _generator.ClearRoom(drawnRoom.x, drawnRoom.y);
                }
                
                _drawnRooms.Clear();

                foreach (var newlyDrawnRoom in _newlyDrawnRooms)
                {
                        _drawnRooms.Add(newlyDrawnRoom);
                }
        }

        private Vector2Int ToRoom(Vector2Int cell)
        {
                cell.x /= _generator.Map.RoomWidth;
                cell.y /= _generator.Map.RoomHeight;

                return cell;
        }

        private bool OutOfBounds(Vector2Int room)
        {
                return room.x < 0 || room.x >= _generator.Map.Width ||
                       room.y < 0 || room.y >= _generator.Map.Height;
        }

        private void Awake()
        {
                _generator = GetComponent<TileMapGenerator>();
        }

        private readonly HashSet<Vector2Int> _drawnRooms = new HashSet<Vector2Int>();
        private readonly HashSet<Vector2Int> _newlyDrawnRooms = new HashSet<Vector2Int>();
        private TileMapGenerator _generator;
}