using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 10f;
    public float xMin = -7f, xMax = 7f;

    [Header("Tiro")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootCooldown = 0.3f;
    private float cooldownTimer = 0f;
    public Color baseBulletColor = Color.white;
    public Color strongBulletColor = Color.red;


    [Header("Vidas")]
    public int Lives = 3;
    private int currentLives;

    private Rigidbody rb;


    private float baseSpeed;
    private float speedTimer = 0f;

    private int baseDamage = 1;
    private int currentDamage;
    private float damageTimer = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

        currentLives = Lives;
        GameManager.Instance.lives = currentLives;
        GameManager.Instance.UpdateUI();

        baseSpeed = moveSpeed;
        currentDamage = baseDamage;
    }

    void Update()
    {
        Move();
        Shoot();
        UpdatePowerUps();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(h * moveSpeed, 0, 0);
        Vector3 newPos = rb.position + movement * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, xMin, xMax);
        rb.MovePosition(newPos);
    }

    void Shoot()
    {
        cooldownTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && cooldownTimer <= 0f)
        {
            GameObject b = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            PlayerBullet pb = b.GetComponent<PlayerBullet>();

            pb.damage = currentDamage;
            pb.SetColor(currentDamage > baseDamage ? strongBulletColor : baseBulletColor);

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
        currentLives = Lives;
        GameManager.Instance.lives = currentLives;
        GameManager.Instance.UpdateUI();
    }

    public void ActivateSpeedBoost(float amount, float duration)
    {
        moveSpeed = baseSpeed + amount;
        speedTimer = duration;
    }

    public void ActivateDamageBoost(int extraDamage, float duration)
    {
        currentDamage = baseDamage + extraDamage;
        damageTimer = duration;
    }

    public void Heal(int amount)
    {
        currentLives += amount;
        GameManager.Instance.lives = currentLives;
        GameManager.Instance.UpdateUI();
    }

    void UpdatePowerUps()
    {
        if (speedTimer > 0)
        {
            speedTimer -= Time.deltaTime;
            if (speedTimer <= 0)
                moveSpeed = baseSpeed;
        }

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
                currentDamage = baseDamage;
        }
    }
}