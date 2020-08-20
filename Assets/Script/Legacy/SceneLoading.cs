using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    AsyncOperation gameLevel;
    [SerializeField]
    GameObject loadingInterface;
    [SerializeField]
    Image loadingProgressBar;


    public void StartButtonLoad()
    {
        loadingInterface.SetActive(true);
        StartCoroutine(LoadTheGame());
    }

    IEnumerator LoadTheGame()
    {
        gameLevel = SceneManager.LoadSceneAsync(1);
        while (!gameLevel.isDone)
        {
            loadingProgressBar.fillAmount = gameLevel.progress;
            yield return null;
        }
    }

}
