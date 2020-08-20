using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeFUTURE : MonoBehaviour
{
    private AbstractTree futureTree = new AbstractTree();

    void Start()
    {
        futureTree.commonName = "FUTURE TREE";
        futureTree.genus = "FUTUREUS";
        futureTree.scientificName = "TREEUS";
        futureTree.facts = "THIS TREE IS FROM THE FUTURE.";
        futureTree.culturalData = "F U T U R E ?";
        futureTree.ID = 2;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!IsPointerOverUIObject())
                {
                    print("FUTURE TREE");
                    Fill_UI._abstractTree = futureTree;
                    Fill_UI.OpenUI();
                }
            }
        }
    }

    //When Touching UI
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
