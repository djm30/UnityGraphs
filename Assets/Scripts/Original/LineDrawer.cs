using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    
    private LineRenderer _lineRenderer;
    public int SourceNode { get; private set; }
    public int DestinationNode { get; private set; }
    
    public void Initialise(int source, int destination)
    {
        _lineRenderer ??= GetComponent<LineRenderer>();
        SourceNode = source;
        DestinationNode = destination;
    }
    

    public void Draw(Transform sourceTransform, Transform destinationTransform)
    {
        _lineRenderer.SetPosition(0, sourceTransform.position);
        _lineRenderer.SetPosition(1, destinationTransform.position);
    }
}
