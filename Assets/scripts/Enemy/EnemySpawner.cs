using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs dos inimigos")]
    public GameObject basicEnemy;
    public GameObject fastEnemy;
    public GameObject tankEnemy;

    [Header("Configuração de Spawn")]
    public float spawnInterval = 2f;
    public float spawnXMin = -7f;
    public float spawnXMax = 7f;
    public float spawnY = 6f;

    [Header("Dificuldade (Opcional)")]
    public float difficultyIncreaseInterval = 10f;  
    public float intervalDecreaseAmount = 0.2f;      
    public float minSpawnInterval = 0.5f;            

    private float timer = 0f;
    private float difficultyTimer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }

        if (difficultyTimer >= difficultyIncreaseInterval)
        {
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - intervalDecreaseAmount);
            difficultyTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        int enemyType = Random.Range(0, 3);

        GameObject prefabToSpawn = null;

        switch (enemyType)
        {
            case 0:
                prefabToSpawn = basicEnemy;
                break;
            case 1:
                prefabToSpawn = fastEnemy;
                break;
            case 2:
                prefabToSpawn = tankEnemy;
                break;
        }

        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}

