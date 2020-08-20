using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public static void openMapMode()
    {
        SceneManager.LoadScene(0);
        //SceneManager.UnloadSceneAsync(1);
    }

    public static void openFPSMode()
    {
        SceneManager.LoadScene(1);
        //SceneManager.UnloadSceneAsync(2);
    }

    public void openMapModeButton()
    {
        SceneManager.LoadScene(0);
    }
}
