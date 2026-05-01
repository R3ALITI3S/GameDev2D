using System.Drawing;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Transform spawnPlane;
    Vector3 spawnPosition;
    public GameObject[] enemySpawns;
    Vector3 randomPosition;
    Vector3 center;
    Vector3 size;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnPlane = GetComponentInChildren<Transform>();
        size = spawnPlane.localScale;
        center = spawnPlane.position;
        SpawnEnemy();

    }

    void SpawnEnemy()
    {
        foreach(GameObject enemy in enemySpawns)
        {
            randomPosition = new Vector3(
            Random.Range(center.x - size.x * 5f, center.x + size.x * 5f), center.y,
            Random.Range(center.z - size.z * 5f, center.z + size.z * 5f)
            );
            Instantiate(enemy, randomPosition, Quaternion.identity);
        }
    }
}
