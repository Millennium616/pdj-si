using UnityEngine;


public class PowerUpManager : MonoBehaviour

{
public static PowerUpManager Instance;


    public GameObject[] powerUps;
    public float dropChance = 0.25f;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }



    public void TrySpawnPowerUp(Vector3 pos)
    {
        float r = Random.value;
        if (r <= dropChance)
        {
            int rand = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[rand], pos, Quaternion.identity);
        }
    }

}
