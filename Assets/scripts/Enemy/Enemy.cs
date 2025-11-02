using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public float speed = 2f;
    public int damage = 1;
    public float shootInterval = 1.5f;
    public GameObject bulletPrefab;
    public Transform shootPoint;

    private float shootTimer;

    void Start()
    {
        shootTimer = shootInterval;
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && shootPoint != null)
        {
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PowerUpManager.Instance?.TrySpawnPowerUp(transform.position);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

