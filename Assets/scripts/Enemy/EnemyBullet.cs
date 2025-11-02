using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
