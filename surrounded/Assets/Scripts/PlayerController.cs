using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Weapon weapon;
    public GameOverScreen gameOverScreen;     // Reference to the GameOverScreen script
    public PauseMenu pauseMenu;
    public int damage = 5;
    public int defense = 3;
    public int moveSpeed = 10;
    public int health = 50;
    public int maxHealth = 50;
    public float XP = 0;
    public int currentLevel = 1;
    public float levelReq = 30 * Mathf.Pow(1.1f, 0);
    public SpriteRenderer sprite;



    Vector2 moveDirection;
    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.Pause();
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButton(0)){
            weapon.Fire();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (XP >= levelReq) {
            LevelUp();
        }
    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void takeDamage(int damage) {
        int damageTaken = (damage - defense);
        if (damageTaken <= 1) {
            damageTaken = 1;
        }
        health -= damageTaken;
        StartCoroutine(FlashRed());

        Debug.Log("Player taking damage! Health: " + health);
        if(health <= 0){
            gameOverScreen.Setup();
            Destroy(gameObject);
        }
    }

    private void LevelUp() {
        damage += 3;
        defense += 3;
        moveSpeed += 3;
        health += 10;
        maxHealth += 10;
        XP -= levelReq;
        currentLevel++;
        levelReq = 30 * Mathf.Pow(1.1f, (currentLevel-1));
    }

    IEnumerator FlashRed(){
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
