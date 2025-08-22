using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        
        FindObjectOfType<TowerManager>().PlaceTower(transform);
    }
}