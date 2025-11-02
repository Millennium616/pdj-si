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
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
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
        Vector3 movement = new Vector3(h * moveSpeed, 0, 0);
        rb.linearVelocity = movement;
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

        Debug.Log("Player TakeDamage chamado! Vidas antes: " + currentLives);
        currentLives -= amount;
        GameManager.Instance.lives = currentLives;
        GameManager.Instance.UpdateUI();
        if (currentLives <= 0)
        {
            if (Application.CanStreamedLevelBeLoaded("Defeat"))
                SceneManager.LoadScene("Defeat");
            else
                Debug.LogWarning("A cena 'Defeat' não existe ou não está adicionada em Build Settings!");
        }
    }
    
    public void ResetPlayer()
    {
        currentLives = maxLives;
        GameManager.Instance.lives = currentLives;
        GameManager.Instance.UpdateUI();
    }
}
