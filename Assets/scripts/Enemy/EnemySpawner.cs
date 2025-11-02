using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 2f;
    public float xMin = -7f, xMax = 7f;
    public float ySpawn = 6f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int idx = Random.Range(0, enemyPrefabs.Length);
        float x = Random.Range(xMin, xMax);
        Vector3 pos = new Vector3(x, ySpawn, 0);
        Instantiate(enemyPrefabs[idx], pos, Quaternion.identity);
    }
}
