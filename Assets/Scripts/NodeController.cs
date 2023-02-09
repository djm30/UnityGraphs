using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class NodeController : MonoBehaviour
    {

        [SerializeField] private GameObject node;
        [SerializeField] private GameObject connector;

        private Transform[] _nodes;
        private readonly SparseMatrix _matrix = SparseMatrix.GetInstance();
        private readonly Dictionary<int, List<GameObject>> connections = new();

        private void Start()
        {

            Node.PositionChanged += OnNodePosChanged;
            
            // Allocating Memory 
            _nodes = new Transform[_matrix.NumNodes()];
            
            for (int i = 0; i < _matrix.NumNodes(); i++)
            {
                var createdNode = Instantiate(node, RandomSpawnLocation(), Quaternion.identity);
                createdNode.GetComponent<Node>().ID = i;
                _nodes[i] = createdNode.transform;
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
            var sourcePos = _nodes[source].position;
            var destPos = _nodes[destination].position;
            
            var direction = destPos - sourcePos;
            var midPoint = direction / 2;

            var spawnPoint = sourcePos + midPoint;
            var connection = Instantiate(connector, spawnPoint, GetConnectorRotation(direction));
            connection.transform.localScale = new Vector3(0.2f,((direction.magnitude) / 2), 0.2f);
            
            if(!connections.ContainsKey(source))
                connections.Add(source, new List<GameObject>());
            
            connections[source].Add(connection);
        }
        
        Quaternion GetConnectorRotation(Vector3 direction)
        {
            return Quaternion.FromToRotation(Vector3.up, direction);
        }
        
        private Vector3 RandomSpawnLocation()
        {
            var range = GetRange();
            var x = Random.Range(-range, range);
            var y = Random.Range(-range, range);
            var z = Random.Range(-range, range);
            return new Vector3(x, y, z);
        }
        private float GetRange() => Mathf.Ceil(Mathf.Pow(_nodes.Length, 1 / 3f)) + 50;


        void OnNodePosChanged(int source)
        {
            RedrawConnections(source, true);
        }
        
        // Not a great solution currently, might be better to store of each connection and modify the transform instead
        // As a lot of overhead is involved in this due to deleting and recreating lines that may not even have been moved
        void RedrawConnections(int source, bool recursive)
        {
            // Destroying every connection attached to this node
            connections[source].ForEach(Destroy);
            
            
            var destinations = _matrix.GetNode(source);
            
            destinations.ForEach(destination =>
            {
                // Redrawing the connection
                DrawConnection(source, destination);
                // Redraws lines of all connected nodes as well
                if(recursive)
                    RedrawConnections(destination, false);
            });
        }
        
        // When redrawing connections after a transform has moved
        // Redraw the connections of all of its connections so duplicate connections can be sorted out
        // either that, or find a way to draw a connection only once some how
    }
}