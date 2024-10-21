using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class RoundTimer : MonoBehaviour
{
    public float roundDuration = 100f;  
    private float timer;                
    public int round = 1;
    public TMP_Text timerText;          
    public TMP_Text roundText;         
    public GameOverScreen gameOverScreen;

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
        round++;
        UpdateRoundText();
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
