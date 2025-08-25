// In TowerSlot.cs
using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [SerializeField]
    private TowerManager towerManager;

    void Start()
    {
        if(towerManager == null)
            towerManager = FindObjectOfType<TowerManager>();
    }

    private void OnMouseDown()
    {
        towerManager.OpenBuildMenu(this);
    }
}