using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Turns
{
    public class EntityGenerator : MonoBehaviour
    {
        [SerializeField, Required] private TurnSystem _turnSystem = default;
        [SerializeField, Required] private Tilemap _tilemap = default;
        [SerializeField, Required] private TileBase _enemyTile = default;
        [SerializeField, Required] private TileBase _treasureTile = default;
        [SerializeField, Required] private CollisionChecker _collisionChecker = default;
        [SerializeField, Required] private TileDrawer _tileDrawer = default;

        private void Start()
        {
            var size = _tilemap.size;
            
            for (var dx = 0; dx < size.x; dx++)
            {
                for (var dy = 0; dy < size.y; dy++)
                {
                    for (var dz = 0; dz < size.z; dz++)
                    {
                        var position = _tilemap.origin + new Vector3Int(dx, dy, dz);
                        if (!_tilemap.HasTile(position)) continue;
                        var tile = _tilemap.GetTile(position);
                        
                        if (tile != _enemyTile) continue;

                        var go = new GameObject {name = ResolveName(tile)};
                        go.transform.parent = _turnSystem.transform;
                        go.transform.SetAsLastSibling();

                        var entity = go.AddComponent<GridEntity>();
                        var entityTile = go.AddComponent<GridEntityTile>();
                        entityTile.Tile = tile;
                        entityTile.TileDrawer = _tileDrawer;

                        var turnMaker = AddTurnMaker(tile, go);
                
                        entity.Position = position;
                
                        _turnSystem.Add(turnMaker);
                    }
                }
            }
        }

        private string ResolveName(TileBase tile)
        {
            if (tile == _enemyTile) return "Enemy";
            if (tile == _treasureTile) return "Treasure";

            return "Unknown Object";
        }

        private ITurnMaker AddTurnMaker(TileBase tile, GameObject go)
        {
            if (tile != _enemyTile) return go.AddComponent<PassiveTurnMaker>();
            
            var turnMaker = go.AddComponent<EnemyTurnMaker>();
            turnMaker.CollisionChecker = _collisionChecker;
            return turnMaker;
        }
    }
}