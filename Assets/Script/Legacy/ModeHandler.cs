using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeHandler : MonoBehaviour
{
    private static GameObject ARMode;
    private static GameObject MapMode;
    // Start is called before the first frame update
    void Start()
    {
        ARMode = GameObject.Find("ARMode");
        MapMode = GameObject.Find("MapMode");
        
    }

    static public void enableARMode()
    {
        ARMode.SetActive(true);
    }

    static public void disableMapMode()
    {
        MapMode.SetActive(false);
    }

    static public void disableARMode()
    {
        ARMode.SetActive(false);
    }

    static public void enableMapMode()
    {
        MapMode.SetActive(true);
    }
}
