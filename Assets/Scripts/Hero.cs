using UnityEngine;

public static class Hero 
{
    
    private static int CountLife = 3;   // колличество жизей
    private static int HpMax=3, HpMin=0;     

    public readonly static float Speed = 3.5f;

    static private float TimeColdown = 3;   // время неуязвимости
    static private float LastInputDamageTime = 0;   // время последнего дамага

    static private int Score = 0;

    static void Die() 
    {
        
        bool newRec = false;
        Time.timeScale = 0;
        if (PlayerPrefs.GetInt("score") < Score)    // сохранение *да можно сделать через Application.persistentDataPath но точечные данные сохранять так удобнее*
        {
            PlayerPrefs.SetInt("score", Score);
            PlayerPrefs.Save();
            newRec = true;
        }

        InGameUI.sing.ShowGameOver(newRec,Score);
    }


    public static void MunisHP() 
    {
        if (Time.time >= LastInputDamageTime)
        {
            LastInputDamageTime = Time.time + TimeColdown;
            CountLife--;
            InGameUI.sing.ShowHp(CountLife);
            if (CountLife <= 0)
                Die();
            else
                InGameUI.sing.ShowInvulnerability();
        }
    }

    public static void PlusHp() 
    {
        CountLife = (int)Mathf.Clamp(CountLife+1, HpMin,HpMax);
        InGameUI.sing.ShowHp(CountLife);
        Score += UnityEngine.Random.Range(5, 25);
        InGameUI.sing.ScoreUpdate(Score);
    }

}
