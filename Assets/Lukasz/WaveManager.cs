using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public string waveName;
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnDelay;
}

public class WaveManager : MonoBehaviour
{
    public List<Wave> waves;
    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private void Start()
    {
        StartNextWave();
    }

    private void Update()
    {
        if (!isSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            currentWaveIndex++;
            StartNextWave();
        }
    }

    void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
        else
        {
            Debug.Log("Wszystkie fale pokonane! Czas na ulepszenia.");
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;
        for (int i = 0; i < wave.enemyCount; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), 6f);
            Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(wave.spawnDelay);
        }
        isSpawning = false;
    }
}