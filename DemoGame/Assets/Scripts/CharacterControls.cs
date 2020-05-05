using UnityEngine;

[RequireComponent(typeof(Character))]
public sealed class CharacterControls : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Position += Vector3Int.left;
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Position += Vector3Int.right;
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Position += Vector3Int.down;
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Position += Vector3Int.up;
    }

    private Vector3Int Position
    {
        get => _character.Position;
        set => _character.Position = value;
    }

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private Character _character;
}