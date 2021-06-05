using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI sing;
    private void Awake()
    {
        sing = this;
    }

    public Text distanceEnemy, distanceBonus;
    public Text score;
    public List<Image> heartList = new List<Image>();
    public GameObject GameOverWindow;
    public Text NewRecord;
    public Text YouRec,RecAllTime;
    public Text AllCurency;
    public GameObject invulnerabilityText;


    public void UpdateDistance(Tuple<string, string> distanceInfo)
    {
        distanceEnemy.text = distanceInfo.Item1 + "m";
        distanceBonus.text = distanceInfo.Item2 + "m";
    }

    public void ShowInvulnerability() 
    {
        invulnerabilityText.SetActive(false);
        invulnerabilityText.SetActive(true);
    }

    public void ShowGameOver(bool rec,int nowScore)
    {
        if (rec)
        {
            NewRecord.enabled = true;
        }
        YouRec.text = "Собрано очков: "+nowScore.ToString();
        RecAllTime.text = "Лучший результат: " + PlayerPrefs.GetInt("score").ToString();

        GameOverWindow.SetActive(true);
    }

    public void RestartButton() 
    {
        Time.timeScale = 1;
        SceneBridg.NameScene = "lvl";
        SceneManager.LoadScene("LoadScene");

    } 

    public void ScoreUpdate(int scoreVal) 
    {
        score.text = scoreVal.ToString() ;
    }

    public void ShowHp(int Hp)
    {
        Hp--;
        for (int i = 2; i >= 0; i--) 
        {
            if(i > Hp)
                heartList[i].enabled = false;
            else
                heartList[i].enabled = true;

        }
    }

    public void ShowInfocurrencity(int count) 
    {
        AllCurency.text = count.ToString();
    }


}
