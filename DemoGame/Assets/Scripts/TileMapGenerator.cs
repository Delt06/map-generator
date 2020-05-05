using System;
using Generator;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public sealed class TileMapGenerator : MonoBehaviour
{
        [SerializeField, Required] private Tilemap _tilemap = default;

        [SerializeField] private Vector2Int _mapSize = Vector2Int.one;
        [SerializeField] private Vector2Int _roomSize = Vector2Int.one;

        [SerializeField, Required, AssetSelector] private RoomTemplates _templates = default;

        [SerializeField, Required, AssetSelector]
        private CellToTileTranslator _cellToTileTranslator = default;

        [SerializeField, Required] private Tilemap _floorTilemap = default;

        [SerializeField, Required, AssetSelector]
        private TileBase _floorTile = default;

        [SerializeField, Range(0f, 1f)] private float _branchProbability = 0.25f;
        [SerializeField, Range(0f, 1f)] private float _minDensity = 0.25f;

        [SerializeField] private bool _generateOnAwake = true;

        [SerializeField] private int _borderInRooms = 1;

        private void Awake()
        {
                if (_generateOnAwake)
                        Generate();
        }

        public Vector2Int WorldToCell(Vector2 worldPosition)
        {
                return (Vector2Int) _tilemap.WorldToCell(worldPosition);
        }

        [Button]
        private void Generate()
        {
                Assert.IsNotNull(_cellToTileTranslator);
                Assert.IsNotNull(_floorTilemap);
                Assert.IsNotNull(_templates);
                Assert.IsNotNull(_floorTile);

                Map = new Map.Builder()
                        .WithSize(_mapSize.x, _mapSize.y)
                        .WithRoomSize(_roomSize.x, _roomSize.y)
                        .Build();

                var generator = new MapGenerator(Map) 
                {
                        BranchProbability = _branchProbability, 
                        MinDensity = _minDensity
                };

                foreach (var templateRoom in _templates.CreateRooms())
                {
                        generator.AddRoomTemplate(templateRoom);
                }

                generator.Generate();
                Map = Map.GetCroppedClone()
                        .GetCloneWithExtraBorderRooms(_borderInRooms)
                        .CreateRemainingRoomsAndFillWithWalls();

                Redraw();
        }

        private void Redraw()
        {
                Clear();
                
                for (var x = 0; x < Map.Width; x++)
                {
                        for (var y = 0; y < Map.Height; y++)
                        {
                                DrawRoom(x, y);
                        }
                }
        }

        [Button]
        private void Clear()
        {
                _tilemap.ClearAllTiles();
                _floorTilemap.ClearAllTiles();
        }

        public Map Map { get; private set; }

        public void DrawRoom(int x, int y)
        {
                if (x < 0 || x >= Map.Width) throw new ArgumentOutOfRangeException($"X must be in range [0, {Map.Width - 1}] but was {x}.");
                if (y < 0 || y >= Map.Height) throw new ArgumentOutOfRangeException($"Y must be in range [0, {Map.Height - 1}] but was {y}.");
                if (!Map.TryGetRoomAt(x, y, out var room)) return;

                var startX = x * Map.RoomWidth;
                var startY = y * Map.RoomHeight;
                
                for (var dx = 0; dx < Map.RoomWidth; dx++)
                {
                        for (var dy = 0; dy < Map.RoomHeight; dy++)
                        {
                                var cell = room[dx, dy];
                                var tile = _cellToTileTranslator.Translate(cell);
                                var position = new Vector3Int(startX + dx, startY + dy, 0);

                                _tilemap.SetTile(position, tile);
                                _floorTilemap.SetTile(position, _floorTile);
                        }
                }
        }

        public void ClearRoom(int x, int y)
        {
                if (x < 0 || x >= Map.Width) return;
                if (y < 0 || x >= Map.Height) return;
                
                var startX = x * Map.RoomWidth;
                var startY = y * Map.RoomHeight;
                
                for (var dx = 0; dx < Map.RoomWidth; dx++)
                {
                        for (var dy = 0; dy < Map.RoomHeight; dy++)
                        {
                                var position = new Vector3Int(startX + dx, startY + dy, 0);
                                
                                _tilemap.SetTile(position, null);
                                _floorTilemap.SetTile(position, null);
                        }
                }
        }
}