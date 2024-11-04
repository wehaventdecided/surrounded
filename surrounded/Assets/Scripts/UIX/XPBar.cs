using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour
{
    public PlayerController p;
    [SerializeField] private Image XP;
    public TMP_Text XPText; 

    // Update is called once per frame
    void Update()
    {
        XPText.text = p.stats.currentLevel.ToString();
        float targetFillAmount = p.stats.XP /p.levelReq;
        XP.fillAmount = targetFillAmount;


    }

}