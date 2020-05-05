using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        
        var direction = new Vector2(x, y);
        direction.Normalize();

        _rigidbody.velocity = direction * _speed;
    }
}