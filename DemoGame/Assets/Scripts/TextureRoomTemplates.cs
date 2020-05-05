using System;
using System.Collections.Generic;
using Generator;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu]
public sealed class TextureRoomTemplates : RoomTemplates
{
    [SerializeField, AssetSelector, Required] private Sprite[] _sprites = default;
    [SerializeField, AssetSelector, Required] private ColorToCellTranslator _translator = default;

    public override IEnumerable<IRoom> CreateRooms()
    {
        foreach (var sprite in _sprites)
        {
            yield return CreateRoomFrom(sprite);
        }
    }

    private IRoom CreateRoomFrom(Sprite sprite)
    {
        Assert.IsNotNull(sprite);
        Assert.IsNotNull(_translator);
        
        var (width, height) = GetSize(sprite);
        var room = Room.OfSize(width, height);

        var (minX, minY) = GetMin(sprite);

        for (var x = 0; x < room.Width; x++)
        {
            for (var y = 0; y < room.Height; y++)
            {
                var pixelX = x + minX;
                var pixelY = y + minY;
                var pixel = sprite.texture.GetPixel(pixelX, pixelY);
                room[x, y] = _translator.Translate(pixel);
            }
        }

        return room;
    }

    private static (int width, int height) GetSize(Sprite sprite)
    {
        var spriteSize = sprite.rect.size;
        var width = Mathf.RoundToInt(spriteSize.x);
        var height = Mathf.RoundToInt(spriteSize.y);

        return (width, height);
    }

    private static (int minX, int minY) GetMin(Sprite sprite)
    {
        var spriteMin = sprite.rect.min;
        var minX = Mathf.RoundToInt(spriteMin.x);
        var minY = Mathf.RoundToInt(spriteMin.y);

        return (minX, minY);
    }
}