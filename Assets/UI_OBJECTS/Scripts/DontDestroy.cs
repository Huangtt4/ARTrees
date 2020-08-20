using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Script has been started");
        DontDestroyOnLoad(transform.gameObject);
    }
   
}
