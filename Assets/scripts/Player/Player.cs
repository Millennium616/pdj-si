using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float shootCooldown = 0.3f;
    public GameObject bulletPrefab;
    public Transform shootPoint;


    private float cooldownTimer;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }


    void Update()
    {
        Move();
        Shoot();
    }


    void Move()
    {
        float h = Input.GetAxis("Horizontal");


        Vector3 dir = new Vector3(h, 0, 0) * moveSpeed;
        rb.linearVelocity = dir;
    }


    void Shoot()
    {
        cooldownTimer -= Time.deltaTime;


        if (Input.GetKey(KeyCode.Space) && cooldownTimer <= 0)
        {
            Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            cooldownTimer = shootCooldown;
        }
    }



    public void TakeDamage(int amount)
    {
        GameManager.Instance.ChangeLives(-amount);
    }

}

