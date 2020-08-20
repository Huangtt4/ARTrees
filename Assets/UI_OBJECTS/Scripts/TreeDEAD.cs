using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeDEAD : MonoBehaviour
{
    private AbstractTree deadTree = new AbstractTree();

    void Start()
    {
        deadTree.commonName = "DEAD TREE";
        deadTree.genus = "DEADUS";
        deadTree.scientificName = "TREEUS";
        deadTree.facts = "THIS TREE IS DEAD.";
        deadTree.culturalData = "D E A D ?";
        deadTree.ID = 3;
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
                    print("DEAD TREE");
                    Fill_UI._abstractTree = deadTree;
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
