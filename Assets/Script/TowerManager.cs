using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject towerPrefab;
    public int towerCost = 100;

    [SerializeField]
    private GameManager gameManager;

    void Start()
    {
        
        if(gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
    }

    
    public void PlaceTower(Transform slotTransform)
    {
        
        if (gameManager.playerCoins >= towerCost)
        {
            
            gameManager.SpendCoins(towerCost);

            
            Instantiate(towerPrefab, slotTransform.position, Quaternion.identity);

            
            slotTransform.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}