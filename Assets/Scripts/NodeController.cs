using System;
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
        
        
        private void Start()
        {
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
            if (source > 127)
            {
                Debug.Log("Huhh?");
            }
            var sourcePos = _nodes[source].position;
            var destPos = _nodes[destination].position;
            
            var direction = destPos - sourcePos;
            var midPoint = direction / 2;

            var spawnPoint = sourcePos + midPoint;
            var connection = Instantiate(connector, spawnPoint, GetConnectorRotation(direction));
            connection.transform.localScale = new Vector3(0.2f,((direction.magnitude) / 2), 0.2f);
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
    }
}