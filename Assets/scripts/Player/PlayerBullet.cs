using UnityEngine;


public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 1;


    void Start()
    {
        Destroy(gameObject, lifeTime);
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
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
