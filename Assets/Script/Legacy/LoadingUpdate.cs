using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUpdate : MonoBehaviour
{
    [SerializeField]
    Text loading;
    int count = 0;
    string text;

    private void Start()
    {
        text = loading.text;
    }
    void Update()
    {
        loading.text = text + count;
        count++;
    }
}
