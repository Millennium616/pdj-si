using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;
    public int points = 1;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootInterval = 2f;
    private float shootTimer;


    void Start()
    {
        currentHealth = maxHealth;
        shootTimer = shootInterval;
    }



    void Update()
    {
        shootTimer -= Time.deltaTime;


        if (shootTimer <= 0)
        {
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            shootTimer = shootInterval;
        }
    }



public void TakeDamage(int amount)
{
currentHealth -= amount;


        if (currentHealth <= 0)
        {



        }
}


    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))

        {
            other.GetComponent<Player>().TakeDamage(1);
        }

    }

    void Die()
    {
        GameManager.Instance.AddScore(points);
        PowerUpManager.Instance.TrySpawnPowerUp(transform.position);
        Destroy(gameObject);
    }


}

