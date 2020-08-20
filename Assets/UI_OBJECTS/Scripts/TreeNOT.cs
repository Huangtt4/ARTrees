using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeNOT : MonoBehaviour
{
    private AbstractTree notTree = new AbstractTree();
    void Start()
    {
        notTree.commonName = "NOT A TREE";
        notTree.genus = "NOTUS";
        notTree.scientificName = "TREEUS";
        notTree.facts = "THIS IS NOT A TREE!";
        notTree.culturalData = null;
        notTree.ID = 4;
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
                    print("NOT A TREE");
                    Fill_UI._abstractTree = notTree;
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
