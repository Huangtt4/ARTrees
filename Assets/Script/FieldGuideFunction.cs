using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGuideFunction : MonoBehaviour
{

    [SerializeField]
    GameObject journalManager;
    [SerializeField]
    GameObject instructionPage;
    [SerializeField]
    GameObject currentTrees;
    [SerializeField]
    Camera mapCamera;

    public void toggle()
    {
        if (mapCamera.enabled == true)
        {
            mapCamera.enabled = false;
            journalManager.SetActive(true);
            currentTrees.SetActive(false);
        }
        else
        {
            journalManager.SetActive(!journalManager.activeSelf);
        }
    }
}
