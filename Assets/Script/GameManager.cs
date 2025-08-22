using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerCoins = 0;
    public int baseHealth = 10;

    [SerializeField]
    private UIManager uiManager; 

    void Start()
    {
        if(uiManager == null)
            uiManager = FindObjectOfType<UIManager>(); 

        // Update the UI at the start 
        uiManager.UpdateCoins(playerCoins);      
        uiManager.UpdateHealth(baseHealth);      
    }

    public void AddCoins(int amount)
    {
        playerCoins += amount;
        uiManager.UpdateCoins(playerCoins); 
    }

    public void SpendCoins(int amount)
    {
        playerCoins -= amount;
        uiManager.UpdateCoins(playerCoins); 
    }

    public void TakeBaseDamage(int amount)
    {
        baseHealth -= amount;
        uiManager.UpdateHealth(baseHealth); 

        if (baseHealth <= 0)
        {
            baseHealth = 0; 
            uiManager.ShowGameOver();
            Time.timeScale = 0f; 
        }
    }
}