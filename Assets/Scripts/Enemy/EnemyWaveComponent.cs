using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveInformation
{
    [SerializeField] public List<GameObject> waveEnemies;
    [SerializeField] public List<int> waveEnemiesAmount;
    [SerializeField] public bool waveSpawned = false;
}

public class EnemyWaveComponent : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<WaveInformation> Waves;
    [SerializeField] bool start = false;

    int currentWave = 0;


    [Header("Reward")]
    [Header("Chest")]
    [SerializeField] public GameObject Chest;
    [SerializeField] public Vector2 chestLocation;
    [Header("High Chance Reward")]
    [SerializeField] public List<GameObject> highChanceItem = new List<GameObject>();
    [SerializeField] public float highChance = 80f;
    [Header("Mid Chance Reward")]
    [SerializeField] public List<GameObject> midChanceItem = new List<GameObject>();
    [SerializeField] public float midChance = 80f;
    [Header("Low Chance Reward")]
    [SerializeField] public List<GameObject> lowChanceItem = new List<GameObject>();
    [SerializeField] public float lowChance = 80f;

    public void Update()
    {
        WaveFunction();
    }

    private void WaveFunction()
    {
        if (start)
        {
            if (Waves.Count > currentWave)
            {
                if (!Waves[currentWave].waveSpawned)
                {
                    int j = 0;
                    foreach (GameObject enemy in Waves[currentWave].waveEnemies)
                    {
                        for (int i = 0; i < Waves[currentWave].waveEnemiesAmount[j]; i++)
                        {
                            int pos = Random.Range(0, 6);
                            GameObject enemySpawned = Instantiate(enemy, spawnPoints[pos]);

                        }
                        j++;
                    }
                    Waves[currentWave].waveSpawned = true;
                }
                else
                {
                    GameObject[] enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy");

                    if (enemiesLeft.Length == 0)
                    {
                        currentWave++;
                    }
                }

            }
            else
            {
                SpawnLoot();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            start = true;
        }
    }
    private void SpawnLoot()
    {
        GameObject chest = Instantiate(Chest, chestLocation, transform.rotation);
        ChestComponent chestComponent = chest.GetComponent<ChestComponent>();

        float random1 = Random.Range(1, 100);
        float random2 = Random.Range(1, 100);
        float random3 = Random.Range(1, 100);

        bool LowChance = false;
        bool MidChance = false;

        // Low Chance Items
        if (random1 < lowChance)
        {
            if (lowChanceItem != null)
            {
                if (lowChanceItem.Count > 0)
                {
                    foreach (GameObject item in lowChanceItem)
                    {
                        chestComponent.items.Add(item);
                    }
                }
                LowChance = true;
            }
        }
        // Mid Chance Items
        if (random2 < midChance && !LowChance)
        {
            if (midChanceItem != null)
            {
                if (midChanceItem.Count > 0)
                {
                    foreach (GameObject item in midChanceItem)
                    {
                        chestComponent.items.Add(item);
                    }
                    MidChance = true;
                }
            }
        }
        // High Chance Items
        if (random3 < highChance && !MidChance && !LowChance)
        {
            if (highChanceItem != null)
            {
                if (highChanceItem.Count > 0)
                {
                    foreach (GameObject item in highChanceItem)
                    {
                        chestComponent.items.Add(item);
                    }
                }
            }
        }
    }

}
