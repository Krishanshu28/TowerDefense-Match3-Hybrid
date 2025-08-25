using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playerCoins = 0;
    public int baseHealth = 10;

    [SerializeField]
    private UIManager uiManager;

    public static GameManager instance;

    void Start()
    {
        if (instance == null)
            instance = this;
        if (uiManager == null)
                uiManager = FindObjectOfType<UIManager>();

        
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
            uiManager.ShowGameOver(false); 
            Time.timeScale = 0f;
        }
    }

    public void HandleVictory()
    {
        uiManager.ShowGameOver(true); 
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}