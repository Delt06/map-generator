using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class CollisionChecker : MonoBehaviour
{
        [SerializeField, Required, ListDrawerSettings(Expanded = true)] 
        private Tilemap[] _treatAsNonEmpty = default;
        
        public bool IsFree(Vector3Int position)
        {
                foreach (var tilemap in _treatAsNonEmpty)
                {
                        if (tilemap.HasTile(position)) return false;
                }

                return true;
        }
}