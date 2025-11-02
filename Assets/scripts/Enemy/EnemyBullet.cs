using UnityEngine;


public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;


    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

}
