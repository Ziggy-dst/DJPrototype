using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab; // 敌人的Prefab
    [SerializeField] private float spawnRate = 5f; // 生成敌人的频率（秒）
    [SerializeField] private Vector3 minSpawnPosition; // 生成区域的最小坐标
    [SerializeField] private Vector3 maxSpawnPosition; // 生成区域的最大坐标

    public int maxEnemyNumber;

    private float timeSinceLastSpawn = 0f; // 距离上次生成敌人的时间

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnRate)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount >= maxEnemyNumber) return;
        Vector3 spawnPosition = new Vector3(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            0,
            Random.Range(minSpawnPosition.z, maxSpawnPosition.z)
        );
        GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length - 1)];
        Instantiate(spawnEnemy, spawnPosition, Quaternion.identity);
    }
}
