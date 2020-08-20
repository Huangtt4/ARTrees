using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeNORMAL : MonoBehaviour
{
    private AbstractTree normalTree = new AbstractTree();

    void Start()
    {
        normalTree.commonName = "NORMAL TREE";
        normalTree.genus = "NORMALUS";
        normalTree.scientificName = "TREEUS";
        normalTree.facts = "THIS TREE IS NORMAL.";
        normalTree.culturalData = "N O R M A L ?";
        normalTree.ID = 1;
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
                    print("NORMAL TREE");
                    Fill_UI._abstractTree = normalTree;
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