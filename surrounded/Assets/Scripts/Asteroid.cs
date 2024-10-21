using UnityEngine;

/**
 * AsteroidController inherits from EnemyController for code reuse and ease of implementation
 */
public class AsteroidController : EnemyController
{

    private Vector3 moveTarget;
    
    /**
     * The start for Asteroid does two things:
     * 1. Finds the target player
     * 2. Uses an impulse force to propel itself towards the player
     */
    void Start()
    {
        /*  It was a good thought. Doesn;t work though.
        Vector2 direction = Random.insideUnitCircle; // should be Vector2 for 2D, yeah?
        transform.Rotate(direction);
        GetComponent<Rigidbody2D>().AddForce(transform.forward, ForceMode2D.Impulse);
        */
        // these four lines sourced and modified from EnemyController and from Bullet
        player = GameObject.FindGameObjectWithTag("Player");
        moveTarget = FindPointNearPlayer();
        Vector3 heading = ((moveTarget + player.transform.position) - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(heading, ForceMode2D.Impulse);
        // asteroid towards player
    }
    
    /**
     * Copied with minor behavioural changes from EnemyController's function of the same name.
     */
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            // do nothing, redundant. handled by Bullet
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // get enemy
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            
            Debug.Log("Asteroid hit player! Inflicting damage!");
            // damage both heavily
            player.takeDamage(50);
            this.takeDamage(50); // stretch goal -- received damage configurable by upgrades
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            // get enemy
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            Debug.Log("Asteroid hit other enemy! Inflicting heavy damage!");
            enemy.takeDamage(100);
            this.takeDamage(100);
        }
    }

    /**
     * The Update method is intentionally left blank in Asteroid because
     * the Asteroid should be entirely static -- it shouldn't have any interactions per-frame.
     * But we need to override EnemyController's Update() for that to happen.
     */
    void Update()
    {
        // this section intentionally left blank
    }
}