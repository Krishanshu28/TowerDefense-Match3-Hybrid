using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Tower Stats")]
    public float range = 5f;
    public float fireRate = 1f; 
    public float damage = 25f;

    [Header("Required Setup")]
    public GameObject projectilePrefab;

    private Transform target;
    private float fireCountdown = 0f;

    private int level = 1;
    private UpgradeManager upgradeManager;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if(upgradeManager == null)
            upgradeManager = FindObjectOfType<UpgradeManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    private void OnMouseDown()
    {
        upgradeManager.ApplyUpgrade(this);
    }
     public void Upgrade()
    {
        level++;
        
        damage *= 1.5f;
        range += 0.5f;

        transform.localScale *= 1.1f;
        spriteRenderer.color = Color.yellow;
    }

    
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }

    void Update()
    {
        
        if (target == null) return;

        
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate; 
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        
        GameObject projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            
            projectile.target = target;
            projectile.damage = this.damage;
        }
    }
}