using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;
    public GameObject[] powerUpPrefabs;
    public float dropChance = 0.3f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TrySpawnPowerUp(Vector3 position)
    {
        if (Random.value < dropChance && powerUpPrefabs.Length > 0)
        {
            int idx = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUp = Instantiate(powerUpPrefabs[idx], position, Quaternion.identity);
        }
    }

}