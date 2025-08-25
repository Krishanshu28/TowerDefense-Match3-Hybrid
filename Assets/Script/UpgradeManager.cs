using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public int availableUpgrades = 0;
    public bool isUpgradeModeActive = false;

    [Header("UI Setup")]
    public Button upgradeButton;
    public TMP_Text upgradeButtonText;

    void Start()
    {
        UpdateUI();
    }

    
    public void AddUpgradeToken()
    {
        availableUpgrades++;
        UpdateUI();
    }

    
    public void ToggleUpgradeMode()
    {
        
        if (availableUpgrades > 0)
        {
            isUpgradeModeActive = !isUpgradeModeActive;

            // Provide visual feedback
            if (isUpgradeModeActive)
            {
                upgradeButtonText.text = "SELECT A TOWER";
                
            }
            else
            {
                UpdateUI();
            }
        }
    }

    
    public void ApplyUpgrade(Tower towerToUpgrade)
    {
        if (availableUpgrades > 0 && isUpgradeModeActive)
        {
            towerToUpgrade.Upgrade();
            availableUpgrades--;
            isUpgradeModeActive = false; 
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        upgradeButtonText.text = $"UPGRADE ({availableUpgrades})";
        upgradeButton.interactable = availableUpgrades > 0;
    }
}