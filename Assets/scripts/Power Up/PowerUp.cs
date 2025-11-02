using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type { ExtraLife, SpeedUp, StrongShot }
    public Type type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                ApplyEffect(player);
            }
            Destroy(gameObject);
        }
    }

    void ApplyEffect(Player player)
    {
        switch (type)
        {
            case Type.ExtraLife:
                GameManager.Instance.ChangeLives(+1);
                break;

            case Type.SpeedUp:
                player.moveSpeed += 3f;
                break;

            case Type.StrongShot:
                player.bulletPrefab.GetComponent<PlayerBullet>().damage += 1;
                break;
        }
    }
}