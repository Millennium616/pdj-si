using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 1;

    private MeshRenderer sphereRenderer;
    private TrailRenderer trailRenderer;
    private Light bulletLight;
    
    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor"); 
    private static readonly int MainColorID = Shader.PropertyToID("_Color"); 

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>(); 
        sphereRenderer = GetComponentInChildren<MeshRenderer>();
        bulletLight = GetComponentInChildren<Light>();

        Destroy(gameObject, lifeTime);
    }

    public void SetColor(Color c)
    {
        if (sphereRenderer != null)
        {
            Material mat = sphereRenderer.material;
            
            if (mat.HasProperty(BaseColorID)) 
            {
                 mat.SetColor(BaseColorID, c);
            }
            else if (mat.HasProperty(MainColorID)) 
            {
                 mat.SetColor(MainColorID, c);
            }
            else
            {
                 mat.color = c;
            }
            
            if (damage > 1 && mat.HasProperty(EmissionColorID))
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor(EmissionColorID, c * 2f); 
            }
            else if (mat.HasProperty(EmissionColorID))
            {
                 mat.SetColor(EmissionColorID, Color.black);
            }
        }
        
        if (trailRenderer != null)
        {
            trailRenderer.startColor = c; 
            trailRenderer.endColor = new Color(c.r, c.g, c.b, 0f);
        }

        if (bulletLight != null)
        {
            bulletLight.color = c;
        }
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