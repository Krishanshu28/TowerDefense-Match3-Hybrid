using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 6;
    public int height = 6;
    public GameObject tilePrefab;
    public Sprite[] tileSprites;
    public float tileMoveSpeed = 0.2f;

    public int coinsPerMatch = 10;

    private Tile[,] allTiles;
    private Tile selectedTile = null;

    public Vector2 gridOffset;

    [SerializeField]
    private GameManager gameManager;

    GameObject gridHolder;

    void Start()
    {
        allTiles = new Tile[width, height];
        if(gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        GenerateGrid();
    }

    void GenerateGrid()
    {
        gridHolder = new GameObject("Grid"); 

        gridHolder.transform.position = gridOffset;

        float xOffset = (width - 1) / 2.0f;
        float yOffset = (height - 1) / 2.0f;
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                
                GameObject newTileObject = Instantiate(tilePrefab, gridHolder.transform);

                
                Vector2 localPosition = new Vector2(x - xOffset, y - yOffset);

                
                newTileObject.transform.localPosition = localPosition;
                
                
                newTileObject.name = $"Tile ({x},{y})";

                Tile newTile = newTileObject.GetComponent<Tile>();
                newTile.x = x;
                newTile.y = y;
                allTiles[x, y] = newTile;
                
                
                List<Sprite> possibleSprites = new List<Sprite>(tileSprites);
                if (x > 1)
                {
                    Sprite sprite1 = allTiles[x - 1, y].GetComponent<SpriteRenderer>().sprite;
                    Sprite sprite2 = allTiles[x - 2, y].GetComponent<SpriteRenderer>().sprite;
                    if (sprite1 == sprite2)
                    {
                        possibleSprites.Remove(sprite1);
                    }
                }
                if (y > 1)
                {
                    Sprite sprite1 = allTiles[x, y - 1].GetComponent<SpriteRenderer>().sprite;
                    Sprite sprite2 = allTiles[x, y - 2].GetComponent<SpriteRenderer>().sprite;
                    if (sprite1 == sprite2)
                    {
                        possibleSprites.Remove(sprite1);
                    }
                }

                int randomSpriteIndex = Random.Range(0, possibleSprites.Count);
                newTile.GetComponent<SpriteRenderer>().sprite = possibleSprites[randomSpriteIndex];
            }
        }
    }

    public void SelectTile(Tile tile)
    {
        if (selectedTile == null)
        {
            selectedTile = tile;
        }
        else
        {
            float distance = Vector2.Distance(selectedTile.transform.position, tile.transform.position);
            if (distance < 1.5f)
            {
                StartCoroutine(SwapAndCheckMatches(selectedTile, tile));
            }
            selectedTile = null;
        }
    }

    private IEnumerator SwapAndCheckMatches(Tile tile1, Tile tile2)
    {
        yield return StartCoroutine(SwapTiles(tile1, tile2));

        List<Tile> matches = FindAllMatches();
        if (matches.Count > 0)
        {
            
            while (matches.Count > 0)
            {
                ClearMatches(matches);
                yield return new WaitForSeconds(0.1f); 

                yield return StartCoroutine(CollapseAndRefill());

                matches = FindAllMatches();
            }
        }
        else
        {
            yield return StartCoroutine(SwapTiles(tile1, tile2)); 
        }
    }

    private IEnumerator SwapTiles(Tile tile1, Tile tile2)
    {
        
        Vector3 tile1Position = tile1.transform.position;
        Vector3 tile2Position = tile2.transform.position;
        tile1.transform.position = tile2Position;
        tile2.transform.position = tile1Position;
        allTiles[tile1.x, tile1.y] = tile2;
        allTiles[tile2.x, tile2.y] = tile1;
        int tempX = tile1.x;
        tile1.x = tile2.x;
        tile2.x = tempX;
        int tempY = tile1.y;
        tile1.y = tile2.y;
        tile2.y = tempY;
        yield return new WaitForSeconds(tileMoveSpeed);
    }
    
    private List<Tile> FindAllMatches()
    {
        List<Tile> matchedTiles = new List<Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile currentTile = allTiles[x, y];
                if (currentTile == null) continue;
                if (x < width - 2)
                {
                    Tile right1 = allTiles[x + 1, y];
                    Tile right2 = allTiles[x + 2, y];
                    if (right1 != null && right2 != null &&
                        currentTile.GetComponent<SpriteRenderer>().sprite == right1.GetComponent<SpriteRenderer>().sprite &&
                        right1.GetComponent<SpriteRenderer>().sprite == right2.GetComponent<SpriteRenderer>().sprite)
                    {
                        if (!matchedTiles.Contains(currentTile)) matchedTiles.Add(currentTile);
                        if (!matchedTiles.Contains(right1)) matchedTiles.Add(right1);
                        if (!matchedTiles.Contains(right2)) matchedTiles.Add(right2);
                    }
                }
                if (y < height - 2)
                {
                    Tile up1 = allTiles[x, y + 1];
                    Tile up2 = allTiles[x, y + 2];
                    if (up1 != null && up2 != null &&
                        currentTile.GetComponent<SpriteRenderer>().sprite == up1.GetComponent<SpriteRenderer>().sprite &&
                        up1.GetComponent<SpriteRenderer>().sprite == up2.GetComponent<SpriteRenderer>().sprite)
                    {
                        if (!matchedTiles.Contains(currentTile)) matchedTiles.Add(currentTile);
                        if (!matchedTiles.Contains(up1)) matchedTiles.Add(up1);
                        if (!matchedTiles.Contains(up2)) matchedTiles.Add(up2);
                    }
                }
            }
        }
        return matchedTiles;
    }

    private void ClearMatches(List<Tile> matchedTiles)
    {
       
        foreach (Tile tile in matchedTiles)
        {
            if (tile != null)
            {
                allTiles[tile.x, tile.y] = null;
                Destroy(tile.gameObject);
            }
        }
        gameManager.AddCoins(matchedTiles.Count * coinsPerMatch);
        
    }

    
    private IEnumerator CollapseAndRefill()
    {
        
        List<Tile> movedTiles = new List<Tile>();
        
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
               
                if (allTiles[x, y] == null)
                {
                    
                    for (int y2 = y + 1; y2 < height; y2++)
                    {
                        if (allTiles[x, y2] != null && !movedTiles.Contains(allTiles[x, y2]))
                        {
                            Tile tileToMove = allTiles[x, y2];
                            
                            
                            allTiles[x, y] = tileToMove;
                            allTiles[x, y2] = null;
                            tileToMove.y = y;
                            
                            
                            float xOffset = (width - 1) / 2.0f;
                            float yOffset = (height - 1) / 2.0f;
                            tileToMove.transform.localPosition = new Vector2(x - xOffset, y - yOffset);
                            
                            movedTiles.Add(tileToMove);
                            break;
                        }
                    }
                }
            }
        }

    
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allTiles[x, y] == null)
                {
                
                    GameObject newTileObject = Instantiate(tilePrefab, gridHolder.transform);

                    float xOffset = (width - 1) / 2.0f;
                    float yOffset = (height - 1) / 2.0f;
                    newTileObject.transform.localPosition = new Vector2(x - xOffset, y - yOffset);
                    
                    newTileObject.name = $"Tile ({x},{y})";

                    Tile newTile = newTileObject.GetComponent<Tile>();
                    newTile.x = x;
                    newTile.y = y;
                    allTiles[x, y] = newTile;
                    
                    newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[Random.Range(0, tileSprites.Length)];
                }
            }
        }
        
        yield return new WaitForSeconds(tileMoveSpeed);
    }
}