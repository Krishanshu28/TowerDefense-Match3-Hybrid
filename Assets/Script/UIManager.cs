using UnityEngine;
using TMPro; // Add this line to use TextMeshPro

public class UIManager : MonoBehaviour
{
    [Header("HUD Elements")]
    public TMP_Text coinsText;
    public TMP_Text healthText;
    public TMP_Text waveText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;

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
}