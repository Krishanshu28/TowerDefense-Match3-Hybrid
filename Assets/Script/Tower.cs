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

    void Start()
    {
        
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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