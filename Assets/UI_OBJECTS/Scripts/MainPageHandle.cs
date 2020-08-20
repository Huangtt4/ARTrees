using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageHandle : MonoBehaviour
{
    public bool active = false;
    public GameObject journalManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void SetJournal()
    {
        active = !active;
        Debug.Log("Active Changed");
        journalManager.SetActive(active);
        GameObject mainPage = journalManager.transform.Find("MainPg").gameObject;
        if (mainPage == null)
        {
            Debug.Log("Cannot find object!");
        }
        else
        {
            mainPage.SetActive(active);
        }
    }
}
