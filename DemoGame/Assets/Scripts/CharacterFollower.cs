using Sirenix.OdinInspector;
using UnityEngine;

public sealed class CharacterFollower : MonoBehaviour
{
    [SerializeField, Required] private GridEntity _entity = default;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxDistance = 25f;
    
    private void Update()
    {
        var currentPosition = _transform.position;
        var oldZ = currentPosition.z;
        currentPosition.z = _entity.Position.z;

        Vector3 newPosition;

        if ((currentPosition - _entity.Position).sqrMagnitude > _maxDistance * _maxDistance)
        {
            newPosition = _entity.Position;
        }
        else
        {
            var deltaPosition = Time.deltaTime * _speed;
            newPosition = Vector3.MoveTowards(currentPosition, _entity.Position, deltaPosition);
        }

        newPosition.z = oldZ;
        
        _transform.position = newPosition;
    }

    private void Awake()
    {
        _transform = transform;
    }

    private Transform _transform;
}