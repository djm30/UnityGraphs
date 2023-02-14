using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeController : MonoBehaviour
{

    [SerializeField] private GameObject node;
    [SerializeField] private GameObject lineRenderer;
    [SerializeField] private GameObject nodeParent;
    [SerializeField] private GameObject lineParent;
        
    private readonly Dictionary<int, List<LineDrawer>> _lines = new();
    private readonly Dictionary<(int,int), LineDrawer> _hasConnection = new();
    private readonly SparseMatrix _matrix = SparseMatrix.GetInstance();
    private Transform[] _nodes;

    private void Start()
    {

        Node.PositionChanged += OnNodePosChanged;
            
        // Allocating Memory 
        _nodes = new Transform[_matrix.NumNodes()];
            
        for (int i = 0; i < _matrix.NumNodes(); i++)
        {
            // Spawning in node
            var createdNode = Instantiate(node, RandomSpawnLocation(), Quaternion.identity);
            createdNode.transform.SetParent(nodeParent.transform);
            createdNode.GetComponent<Node>().ID = i;
            _nodes[i] = createdNode.transform;
                
            // Assigning each node a list to store lines to it connects to/ connects to itself
            _lines.Add(i, new List<LineDrawer>());
        }
            
        DrawConnections();
    }
        
    private void DrawConnections()
    {
        for (int i = 0; i < _nodes.Length; i++)
        {
            var destinations = _matrix.GetNode(i);
            for (int j = 0; j < destinations.Count; j++)
            {
                DrawConnection(i, destinations[j]);
            }
        }
    }

    private void DrawConnection(int source, int destination)
    {
        // Checking if this node pair already has a connection
        if (_hasConnection.TryGetValue(GetConnection(source, destination), out var renderedLine))
        {
            // If a connection has already been drawn, it is added to the list of connections for the source node
            _lines[source].Add(renderedLine);
            return;
        }
            
        // Creating game object with line renderer
        var line = Instantiate(lineRenderer);
            
        var lineDrawer = line.GetComponent<LineDrawer>();
            
        lineDrawer.Initialise(source, destination);
        lineDrawer.Draw(_nodes[lineDrawer.SourceNode], _nodes[lineDrawer.DestinationNode]);
        
        line.transform.SetParent(lineParent.transform);
            
        
        // Setting that this node pair now already have a connection drawn between them
        _hasConnection.Add(GetConnection(source, destination), lineDrawer);
        // Adding that line to the list of lines that connect to the source node
        _lines[source].Add(lineDrawer);
        if(!_lines[destination].Contains(lineDrawer))
            // Adding the line to the list of lines that connect to the destination node given it isnt already present
            _lines[destination].Add(lineDrawer);
    }
    
    private Vector3 RandomSpawnLocation()
    {
        var range = GetRange();
        var x = Random.Range(-range, range);
        var y = Random.Range(-range, range);
        var z = Random.Range(-range, range);
        return new Vector3(x, y, z);
    }

    private float GetRange() => Mathf.Log(_nodes.Length * 2) * 20f;



    void OnNodePosChanged(int source)
    {
        // Redraws all lines after the transform of the source node has changed
        _lines[source].ForEach(drawer =>
        {
            drawer.Draw(_nodes[drawer.SourceNode], _nodes[drawer.DestinationNode]);
        });
    }
    
        
    (int, int) GetConnection(int source, int destination)
    {
        return source < destination ? (source, destination) : (destination, source);
    }
}