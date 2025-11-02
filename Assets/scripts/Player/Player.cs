using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 10f;

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootCooldown = 0.3f;
    private float cooldownTimer = 0f;

    [Header("Vidas")]
    public int maxLives = 3;
    private int currentLives;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        currentLives = maxLives;
        GameManager.Instance.lives = currentLives;
        GameManager.Instance.UpdateUI();
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical"); // caso queira mover verticalmente também

        Vector3 movement = new Vector3(h, 0, 0) * moveSpeed;
        rb.linearVelocity = movement; // Movimento no eixo X

        // Se quiser mover no eixo Z (frente e trás) em vez do Y (altura), use:
        // Vector3 movement = new Vector3(h, 0, v) * moveSpeed;
        // rb.velocity = movement;
    }

    void Shoot()
    {
        cooldownTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && cooldownTimer <= 0f)
        {
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            cooldownTimer = shootCooldown;
        }
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        GameManager.Instance.lives = currentLives;
        GameManager.Instance.UpdateUI();
        if (currentLives <= 0)
        {
            SceneManager.LoadScene("Defeat");
        }
    }

    public void ResetPlayer()
    {
        moveSpeed = 10f;
        shootCooldown = 0.3f;
        currentLives = maxLives;
        GameManager.Instance.lives = currentLives;
        GameManager.Instance.UpdateUI();
    }
}
