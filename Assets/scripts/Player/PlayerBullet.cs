using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 1;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        Destroy(gameObject, lifeTime);
    }

    public void SetColor(Color c)
    {
        if (rend != null)
            rend.material.color = c;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
