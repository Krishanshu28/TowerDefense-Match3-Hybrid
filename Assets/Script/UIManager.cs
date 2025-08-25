using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public TMP_Text coinsText;
    public TMP_Text healthText;
    public TMP_Text waveText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText; 
    public GameObject victoryPanel; 

    public void UpdateCoins(int amount)
    {
        coinsText.text = "Coins: " + amount;
    }

    public void UpdateHealth(int amount)
    {
        healthText.text = "Health: " + amount;
    }

    public void UpdateWave(int currentWave, int totalWaves)
    {
        waveText.text = $"Wave: {currentWave} / {totalWaves}";
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
     public void ShowGameOver(bool didWin)
    {
        gameOverPanel.SetActive(true);
        if (didWin)
        {
            gameOverText.text = "VICTORY!";
        }
        else
        {
            gameOverText.text = "DEFEAT!";
        }
    }
    
}