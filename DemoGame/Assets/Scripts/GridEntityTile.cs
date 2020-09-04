using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(IGridEntity))]
public sealed class GridEntityTile : MonoBehaviour
{
    [SerializeField, Required, PreviewField] private TileBase _tile = default;
    [SerializeField, Required] private TileDrawer _tileDrawer = default;

    public TileBase Tile
    {
        get => _tile;
        set => _tile = value;
    }

    public TileDrawer TileDrawer
    {
        get => _tileDrawer;
        set => _tileDrawer = value;
    }

    private void OnEnable()
    {
        if (_tileDrawer)
            _tileDrawer.Draw(_entity.Position, _tile);
        
        _entity.PositionChanged += _onPositionChanged;
    }

    private void OnDisable()
    {
        if (_tileDrawer)
            _tileDrawer.Clear(_entity.Position);
        
        _entity.PositionChanged -= _onPositionChanged;
    }

    private void Awake()
    {
        _entity = GetComponent<IGridEntity>();
        Assert.IsNotNull(_entity);

        _onPositionChanged = (sender, args) =>
        {
            _tileDrawer.Clear(args.Old);
            _tileDrawer.Draw(args.New, _tile);
        };
    }

    private EventHandler<PositionChangeArgs> _onPositionChanged;
    private IGridEntity _entity;
}