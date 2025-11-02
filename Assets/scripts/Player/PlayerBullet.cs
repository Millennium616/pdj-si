using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 1;

    private MeshRenderer sphereRenderer;
    private Light bulletLight;
    
    private Color baseLightColor;
    private bool isStrongShot = false;
    
    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor"); 
    private static readonly int MainColorID = Shader.PropertyToID("_Color"); 


    void Start()
    {
        sphereRenderer = GetComponentInChildren<MeshRenderer>();
        bulletLight = GetComponentInChildren<Light>();
        
        Destroy(gameObject, lifeTime);
    }

    public void SetColor(Color c)
    {
        baseLightColor = c;
        
        if (damage > 1)
        {
            isStrongShot = true;
        }

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
            if (!isStrongShot)
            {
                bulletLight.color = c;
                bulletLight.intensity = 500f; // Define a intensidade base
            }
            else
            {
                bulletLight.color = c; 
                bulletLight.intensity = 500f; 
            }
        }
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        if (isStrongShot && bulletLight != null)
        {
            float hue = Mathf.Repeat(Time.time * 0.25f, 1f); 
            
            Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f); 

            bulletLight.color = rainbowColor;

            // CORREÇÃO: Ajusta o range de pulsação para a base 500
            float pulse = (Mathf.Sin(Time.time * 10f) + 1f) * 0.5f;
            bulletLight.intensity = Mathf.Lerp(400f, 600f, pulse);
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