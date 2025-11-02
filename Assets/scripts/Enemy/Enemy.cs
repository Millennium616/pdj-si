using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Basic, ZigZag, Tank }
    public EnemyType enemyType;

    [Header("Status")]
    public int health = 1;
    public float speed = 2f;
    public int damage = 1;

    public int points = 1;

    [Header("Tiro")]
    public float shootInterval = 1.5f;
    public GameObject bulletPrefab;
    public Transform shootPoint;

    private float shootTimer;

    private float startX;
    public float zigzagOffset = 2f;
    public float zigzagSpeed = 3f;

    void Start()
    {
        shootTimer = shootInterval;
        startX = transform.position.x;
    }

    void Update()
    {
        Move();
        HandleShooting();
    }

    void Move()
    {
        switch (enemyType)
        {
            case EnemyType.Basic:
                transform.position += Vector3.down * speed * Time.deltaTime;
                break;

            case EnemyType.ZigZag:
                float oscillate = Mathf.Sin(Time.time * zigzagSpeed) * zigzagOffset;

                transform.position = new Vector3(
                    startX + oscillate,
                    transform.position.y - speed * Time.deltaTime,
                    0f
                );
                break;

            case EnemyType.Tank:
                transform.position += Vector3.down * (speed * 0.5f) * Time.deltaTime;
                break;
        }
    }

    void HandleShooting()
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
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
                rb.linearVelocity = Vector3.down * 8f;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
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
            Player p = other.GetComponent<Player>();
            if (p != null)
                p.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
