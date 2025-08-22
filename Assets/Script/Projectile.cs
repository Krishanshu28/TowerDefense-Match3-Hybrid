using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 25f;
    public Transform target;

    void Update()
    {
        
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.transform == target)
        {
            
            target.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}