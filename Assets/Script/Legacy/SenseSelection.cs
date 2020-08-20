using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseSelection : MonoBehaviour
{
    private static GameObject MapMode;
    private static GameObject FPMode;
    private static GameObject UI;

    public void Start() { 
        MapMode = GameObject.Find("MapMode");
        FPMode = GameObject.Find("FPMode");
        UI = GameObject.Find("UI");
        //hideFPMode();
        //hideUI();

        //hideMapMode();
        showMapMode();
        showFPMode();
        hideFPMode();
    }
    public static void  hideMapMode() 
    {
        MapMode.SetActive(false);
    }

    public static void showMapMode()
    {
        MapMode.SetActive(true);
    }

    public static void hideFPMode()
    {
        FPMode.SetActive(false);
    }

    public static void showFPMode()
    {
        FPMode.SetActive(true);
    }

    public static void hideUI()
    {
        UI.SetActive(false);
    }

    public static void showUI()
    {
        UI.SetActive(true);
    }
}
