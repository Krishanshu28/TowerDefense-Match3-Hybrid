using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Header("Wave Configuration")]
    public Wave[] waves; 
    public Transform[] waypoints;
    public float timeBetweenWaves = 5f;

    [Header("Required Setup")]
    public GameObject enemyPrefab;

    private int currentWaveIndex = 0;

    [SerializeField]
    private UIManager uiManager;
    private bool isSpawning = false;

    void Start()
    {
        if(uiManager == null)
            uiManager = FindObjectOfType<UIManager>();
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        
        for (currentWaveIndex = 0; currentWaveIndex < waves.Length; currentWaveIndex++)
        {
            uiManager.UpdateWave(currentWaveIndex + 1, waves.Length);
            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            yield return new WaitForSeconds(timeBetweenWaves);
        }

        
        while (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            yield return null;
        }

        
        GameManager.instance.HandleVictory();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            GameObject newEnemyObj = Instantiate(enemyPrefab);
            Enemy enemy = newEnemyObj.GetComponent<Enemy>();

            
            enemy.SetWaypoints(waypoints);
            enemy.Setup(wave.enemyHealth, wave.enemySpeed);

            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }
}