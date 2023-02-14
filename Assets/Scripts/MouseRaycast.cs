using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    private Ray ray;
    private Node currentNode;
    
    void Start()
    {
        // ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    
    void Update()
    {
        // ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("Mouse button clicked");
        //     CheckCollision();
        // }
    }

    void CheckCollision()
    {
        if (Physics.Raycast(ray, out var hit))
        {
            currentNode?.Deselect();
            currentNode = hit.collider.gameObject.GetComponent<Node>();
            currentNode.Select();
        }
        else
        {
            currentNode?.Deselect();
            currentNode = null;
        }
    }
}
