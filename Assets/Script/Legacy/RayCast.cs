using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour
{
    GameObject currentTrees;
    GameObject removedTrees;
    GameObject futureTrees;
    // Start is called before the first frame update
    void Start()
    {
        currentTrees = GameObject.Find("current");
        removedTrees = GameObject.Find("removed");
        futureTrees = GameObject.Find("future");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    var selection = hitInfo.transform;
                    if (selection.CompareTag("Tree"))
                    {
                        Trees selectionScript = selection.GetComponent<Trees>();
                        if (selectionScript != null)
                        {
                            Debug.Log("tree");
                            if (!IsPointerOverUIObject())
                            {
                                selectionScript.UpdateUI();
                                currentTrees.SetActive(false);
                                //if (removedTrees != null)
                                //{
                                //    removedTrees.SetActive(false);
                                //}
                                //if (futureTrees != null)
                                //{
                                //    futureTrees.SetActive(false);
                                //}
                                Camera.main.enabled = false;
                                

                            }

                        }
                    }
                }
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
