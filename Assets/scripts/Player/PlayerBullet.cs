using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 1;

    private MeshRenderer sphereRenderer;
    private Light bulletLight;
    
    private Color startColor;
    private Color strongPulseColor = Color.yellow;
    private bool isStrongShot = false;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor"); 
    private static readonly int MainColorID = Shader.PropertyToID("_Color"); 


    void Start()
    {
        sphereRenderer = GetComponentInChildren<MeshRenderer>();
        bulletLight = GetComponentInChildren<Light>();
        
        if (damage > 1)
        {
            isStrongShot = true;
        }

        Destroy(gameObject, lifeTime);
    }

    public void SetColor(Color c)
    {
        startColor = c;

        if (sphereRenderer != null)
        {
            Material mat = sphereRenderer.material;
            
            if (mat.HasProperty(BaseColorID)) 
            {
                 mat.SetColor(BaseColorID, c);
            }
            if (mat.HasProperty(MainColorID)) 
            {
                 mat.SetColor(MainColorID, c);
            }
            
            if (mat.HasProperty(EmissionColorID))
            {
                if (damage > 1)
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor(EmissionColorID, c * 5f); 
                }
                else
                {
                    mat.SetColor(EmissionColorID, Color.black);
                    mat.DisableKeyword("_EMISSION");
                }
            }
        }
        
        if (bulletLight != null)
        {
            bulletLight.color = c;
            bulletLight.intensity = 1f;
        }
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if (isStrongShot && bulletLight != null)
        {
            float t = (Mathf.Sin(Time.time * 20f) + 1f) * 0.5f;
            bulletLight.color = Color.Lerp(startColor, strongPulseColor, t);
            
            bulletLight.intensity = Mathf.Lerp(1.5f, 3.5f, t);
        }
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