using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTrees : MonoBehaviour
{
    [SerializeField]
    GameObject parentObject;

    public void ChangeParent()
    {
        bool onOffSwtich = gameObject.GetComponent<Toggle>().isOn;
        if( onOffSwtich)
        {
            parentObject.transform.position = new Vector3(-999, -999, -999);
        }
        else
        {
            parentObject.transform.position = new Vector3(0, 0, 0);
        }
    }
}
