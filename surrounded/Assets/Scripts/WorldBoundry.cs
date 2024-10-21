using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldBoundary : MonoBehaviour
{
   public GameOverScreen gameOverScreen;
   private Coroutine dot;

   public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController target = other.gameObject.GetComponent<PlayerController>();
            int dmg = (target.health)/3;
            dot = StartCoroutine(DOT(target,dmg));
         }
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.takeDamage(enemy.health/2);
        }
    }
    public void OnCollisionExit2D(Collision2D other){
        StopCoroutine(dot);
    }
    private IEnumerator DOT(PlayerController target, int damage){
        while(true){
            target.takeDamage(damage);
            yield return new WaitForSeconds(1);
        }
    }

    
}