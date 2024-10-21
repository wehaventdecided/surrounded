using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverScreen : MonoBehaviour
{
    public bool gameOver = false;
    public void Setup(){
        gameObject.SetActive(true);
        gameOver = true;
    }

    public void RestartButton(){
        SceneManager.LoadScene("Game");
    }
    public void ExitButton(){
        SceneManager.LoadScene("Main Menu");
    }
}
