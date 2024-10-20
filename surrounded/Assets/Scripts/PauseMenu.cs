using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Pause(){
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume(){
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Stats(){
        //Nothing YET
        }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Quit(){
        SceneManager.LoadScene("Main Menu");       
        Time.timeScale = 1;
    }
}
