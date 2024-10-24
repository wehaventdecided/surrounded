using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class RoundTimer : MonoBehaviour
{
    private float roundDuration = 105f;  
    private float timer;                
    public int round = 1;
    public TMP_Text timerText;          
    public TMP_Text roundText;         
    public GameOverScreen gameOverScreen;
    public PlayerController player;
    public UpgradeScreen upgrade;
    public TMP_Text stats;
    void Start()
    {
        timer = 105; 
        UpdateRoundText();      
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && gameOverScreen.gameOver == false)
        {
            IncrementRound();
            timer = roundDuration; 
        }
        if(gameOverScreen.gameOver == false){
            UpdateTimerText();
        }
        else{
            return;
        }
    }

    void IncrementRound()
    {
        player.health = player.maxHealth;
        round++;
        UpdateRoundText();
        upgrade.DisplayRandomUpgrades();
        stats.text = "ATK: " + player.damage + " DEF: " + player.defense + " SPD: " + player.moveSpeed;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(timer).ToString();
    }

    void UpdateRoundText()
    {
        roundText.text = round.ToString();
    }
    
}
