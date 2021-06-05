using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSc : MonoBehaviour
{
    public Image progressImage;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        Color imgColor = progressImage.color;
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneBridg.NameScene);
        while (!operation.isDone)
        {
            float progress = operation.progress;
            Debug.Log(progress);

            progressImage.color = new Color(imgColor.r, imgColor.g, imgColor.b, 1 - progress);
            yield return null;
        }
    }
}

public static class SceneBridg 
{
    public static string NameScene;
}