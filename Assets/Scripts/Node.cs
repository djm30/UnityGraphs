using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] public int ID;


    private void Update()
    {
        if (transform.hasChanged)
        {
            Debug.Log("Position has changed");
        }
    }
}
