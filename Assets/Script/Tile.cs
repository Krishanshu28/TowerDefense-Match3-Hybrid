using UnityEngine;

public class Tile : MonoBehaviour
{
    // The tile's position in the grid array
    public int x;
    public int y;
    
    private GridManager gridManager;

    void Start()
    {
        
        gridManager = FindObjectOfType<GridManager>();
    }

    
    private void OnMouseDown()
    {
        
        gridManager.SelectTile(this);
    }
}