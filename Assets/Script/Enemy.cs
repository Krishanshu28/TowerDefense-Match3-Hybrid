using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float speed = 2f;

    private Transform[] waypoints;
    private int waypointIndex = 0;

    
    public void SetWaypoints(Transform[] pathWaypoints)
    {
        waypoints = pathWaypoints;
        transform.position = waypoints[0].position; // Teleport to the start
    }

    void Update()
    {
        if (waypoints == null) return;

        // Move towards the current waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);

        
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                //Reached End
                FindObjectOfType<GameManager>().TakeBaseDamage(1);
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}