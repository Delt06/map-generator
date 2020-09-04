using UnityEngine;

public sealed class GridControls : MonoBehaviour
{
    public Vector3Int Motion { get; private set; }
    
    private void Update()
    {
        Motion = Vector3Int.zero;
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Motion = Vector3Int.left;
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Motion = Vector3Int.right;
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Motion = Vector3Int.down;
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Motion = Vector3Int.up;
    }
}