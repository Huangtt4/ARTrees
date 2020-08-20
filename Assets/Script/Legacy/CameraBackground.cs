using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackground : MonoBehaviour
    
{
    public static GameObject background;
    // Start is called before the first frame update
    void Start()
    {

        background = transform.GetChild(0).gameObject;
        background.SetActive(false);
    }

    public static void OpenCamera()
    {
        background.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
