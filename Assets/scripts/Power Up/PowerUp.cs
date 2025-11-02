using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type { ExtraLife, SpeedUp, StrongShot }
    public Type type;

    public float duration = 5f;
    public int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (type)
                {
                    case Type.ExtraLife:
                        player.Heal(amount);
                        break;

                    case Type.SpeedUp:
                        player.ActivateSpeedBoost(amount, duration);
                        break;

                    case Type.StrongShot:
                        player.ActivateDamageBoost(amount, duration);
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
