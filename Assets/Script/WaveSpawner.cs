using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;

    [Header("Wave Settings")]
    public int numberOfWaves = 3;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 5f;

    private int currentWave = 1;

    [SerializeField]
    private UIManager uiManager;

    void Start()
    {
        if(uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        for (currentWave = 1; currentWave <= numberOfWaves; currentWave++)
        {
            uiManager.UpdateWave(currentWave, numberOfWaves);
            yield return StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        
        Debug.Log("YOU WIN!");
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject newEnemyObj = Instantiate(enemyPrefab);
            newEnemyObj.GetComponent<Enemy>().SetWaypoints(waypoints);
            yield return new WaitForSeconds(1f); 
        }
    }
}