using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSc : MonoBehaviour
{
    public void StartGame() 
    {
        SceneBridg.NameScene = "lvl";
        SceneManager.LoadScene("LoadScene");
    }
    public void Exit() 
    {
        Application.Quit();
    }
}
