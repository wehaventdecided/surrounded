using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float fireRate = 2f; // Time between shots
    public int health = 3;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public float shootingRange = 10f;
    public float targetInnerRadius = 7, targetOuterRadius = 10; //inner and outer radius of target ring to move to around player
    public SpriteRenderer sprite;
    private Color originalColor;
    protected GameObject player; // changed from private to protected
    private float nextFireTime = 0f;
    public float XPdropped;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Assuming the player has the tag "Player"
        moveTarget = FindPointNearPlayer();
    }

    void Update()
    {   
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);  
            MoveNearPlayer();
            RotateTowardsPlayer();
            if (distanceToPlayer <= shootingRange && Time.time >= nextFireTime)
            {
                ShootAtPlayer();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    private void MoveTowardsPlayer() //moves enemy towards player in a straight line
    {
        //  Vector2 direction = (player.transform.position - transform.position).normalized; 
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, maxSpeed * Time.deltaTime);
    }

    private void RotateTowardsPlayer()
    {
        Vector2 aimDirection = player.transform.position - transform.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    public float avoidanceRadius = 3; //Radius of avoidance area around enemy
    private Vector3 moveTarget;
    private Vector3 heading;
    protected void MoveNearPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position + moveTarget) < 1)  //Enemy is near target position
        {
            moveTarget = FindPointNearPlayer();
        } 
        heading = ((moveTarget + player.transform.position) - transform.position).normalized;   
        Collider2D[] NearbyColliders = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);
        foreach (Collider2D collider in NearbyColliders)
        {
            
            if (collider.gameObject != this.gameObject && !collider.gameObject.CompareTag("Bullet"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                heading += avoidanceRadius / distance * Vector3.Normalize(transform.position - collider.transform.position);
            }
        }
        heading = heading.normalized;
        transform.position += maxSpeed * Time.deltaTime * heading;
    }


    protected Vector2 FindPointNearPlayer() // changed from private to protected
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized; //vector pointing towards direction of target around player
        float randomRadius = Random.Range(targetInnerRadius, targetOuterRadius); //distance target is from player
        return randomDirection * randomRadius;
    }

    private void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    public void takeDamage(int damage) {
        PlayerController p = player.GetComponent<PlayerController>(); 
        StartCoroutine(FlashRed());
        health -= damage;
        Debug.Log("Enemy health: " + health); // debug message to track health

        if (health <= 0 && p != null) {
            Debug.Log("Enemy destroyed!");
            p.XP += XPdropped;
            Destroy(gameObject);
        }
    }
    /**
     * Code for inter-ship collisions.
     * @param other a Collision2D object containing 
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
            
            Debug.Log("Enemy hit player! Inflicting damage!");
            // damage both heavily
            player.takeDamage(50);
            this.takeDamage(50); // stretch goal -- received damage configurable by upgrades
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            // get enemy
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            Debug.Log("Enemy hit other enemy! Inflicting heavy damage!");
            enemy.takeDamage(100);
            this.takeDamage(100);
        }
    }
    IEnumerator FlashRed(){
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}


