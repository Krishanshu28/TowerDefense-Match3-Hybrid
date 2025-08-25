using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class TowerManager : MonoBehaviour
{
    [Header("Tower Settings")]
    public GameObject towerPrefab;
    public int towerCost = 100;

    [Header("UI Setup")]
    public GameObject buildMenuPanel;
    public Button towerBuildButton;


    private TowerSlot selectedSlot;

    void Start()
    {
        buildMenuPanel.SetActive(false);
    }
    
    public void OpenBuildMenu(TowerSlot slot)
    {
        
        selectedSlot = slot;

        
        buildMenuPanel.transform.position = Camera.main.WorldToScreenPoint(slot.transform.position);
        buildMenuPanel.SetActive(true);

        
        UpdateBuildButton();
    }

    private void UpdateBuildButton()
    {
        
        if (GameManager.instance.playerCoins < towerCost)
        {
            towerBuildButton.interactable = false;
        }
        else
        {
            towerBuildButton.interactable = true;
        }
    }

    
    public void BuildTower()
    {
        if (GameManager.instance.playerCoins >= towerCost)
        {
            GameManager.instance.SpendCoins(towerCost);
            Instantiate(towerPrefab, selectedSlot.transform.position, Quaternion.identity);
            
            
            selectedSlot.gameObject.SetActive(false);
            CloseBuildMenu();
        }
    }

    
    public void CloseBuildMenu()
    {
        buildMenuPanel.SetActive(false);
        selectedSlot = null;
    }
}