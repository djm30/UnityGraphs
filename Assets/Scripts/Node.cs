using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] public int ID;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;
    public static event Action<int> PositionChanged; 

    private Vector3 _currentPosition;
    
    private void Start()
    {
        _currentPosition = transform.position;
    }

    private void Update()
    {
        if (_currentPosition != transform.position)
        {
            OnPositionChanged(ID);
        }

        _currentPosition = transform.position;
    }

    private static void OnPositionChanged(int obj)
    {
        PositionChanged?.Invoke(obj);
    }

    private void OnMouseDown()
    {
        Debug.Log("Clicked on node " + ID);
        GetComponent<MeshRenderer>().material = selectedMaterial;
    }
}
