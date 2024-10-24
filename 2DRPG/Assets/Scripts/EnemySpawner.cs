using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPun
{
    public string enemyPrefabPath;
    public string bossPrefabPath;
    public float maxEnemies;
    public float maxBoss;
    public float spawnRadius;
    public float spawnCheckTime;
    private float lastSpawnCheckTime;
    private List<GameObject> curEnemies = new List<GameObject>();
    private List<GameObject> curBoss = new List<GameObject>();

    public float bossSpawnTime;
    private float lastBossSpawnTime;

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        if (Time.time - lastSpawnCheckTime > spawnCheckTime)
        {
            lastSpawnCheckTime = Time.time;
            TrySpawn();
        }

        if (Time.time - lastBossSpawnTime > bossSpawnTime)
        {
            lastBossSpawnTime = Time.time;
            TryBossSpawn();

        }
    }

    void TrySpawn()
    {
       
        for (int x = 0; x < curEnemies.Count; ++x)
        {
            if (!curEnemies[x])
                curEnemies.RemoveAt(x);
        }
       
        if (curEnemies.Count >= maxEnemies)
            return;
      
        Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;
        GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabPath, transform.position + randomInCircle, Quaternion.identity);
        curEnemies.Add(enemy);
    }

    void TryBossSpawn()
    {

        for (int x = 0; x < curBoss.Count; ++x)
        {
            if (!curBoss[x])
                curBoss.RemoveAt(x);
        }

        if (curBoss.Count >= maxBoss)
            return;

        Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;
        GameObject boss = PhotonNetwork.Instantiate(bossPrefabPath, transform.position + randomInCircle, Quaternion.identity);
        curEnemies.Add(boss);
    }
}
