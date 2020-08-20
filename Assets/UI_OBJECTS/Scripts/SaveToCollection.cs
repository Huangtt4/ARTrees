using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveToCollection : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Text collectionpopup;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveToCollectionPopup()
    {
        collectionpopup.text = "Coming Soon!";
    }
}
