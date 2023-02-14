using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRaycast : MonoBehaviour
{
    private Ray ray;
    private Node currentNode;
    
    void Start()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    }
    
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Mouse.current.leftButton.isPressed)
        {
            CheckCollision();
        }
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
