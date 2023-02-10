using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] public int ID;
    public static event Action<int> PositionChanged; 

    private Vector3 _currentPosition;
    
    private void Start()
    {
        _currentPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position - _currentPosition != Vector3.zero)
        {
            OnPositionChanged(ID);
        }

        _currentPosition = transform.position;
    }

    private static void OnPositionChanged(int obj)
    {
        PositionChanged?.Invoke(obj);
    }
}
