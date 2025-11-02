using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Basic, ZigZag, Tank }
    public EnemyType enemyType;

    public int health = 1;
    public float speed = 2f;
    public int damage = 1;
    public int points = 1;

    [Header("Tiro")]
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
        Move();
        ShootTimer();
    }

    void Move()
    {
        switch (enemyType)
        {
            case EnemyType.Basic:
                transform.position += Vector3.down * speed * Time.deltaTime;
                break;

            case EnemyType.ZigZag:
                float x = Mathf.Sin(Time.time * 3f) * 2f;
                transform.position += new Vector3(x, -speed * Time.deltaTime, 0);
                break;

            case EnemyType.Tank:
                transform.position += Vector3.down * (speed * 0.5f) * Time.deltaTime;
                break;
        }
    }

    void ShootTimer()
    {
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
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.down * 8f;
            }
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
        GameManager.Instance.AddScore(points);
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