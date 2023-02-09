using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private GameObject connector;
    
    public int ID { get; set; }
    public List<GameObject> connectedNodes;
    private GameObject connection;

    void Start()
    {
        connectedNodes.ForEach(otherNode =>
        {
            var position = transform.position;
            
            var direction = otherNode.transform.position - position;
            var midPoint = direction / 2;

            var spawnPoint = position + midPoint;
            connection = Instantiate(connector, spawnPoint, GetConnectorRotation(direction));
            connection.transform.localScale = new Vector3(0.2f,((direction.magnitude) / 2), 0.2f);
        });
        
    }

    void Update()
    {
        connectedNodes.ForEach(otherNode =>
        {
            var position = transform.position;
            
            var direction = otherNode.transform.position - position;
            var midPoint = direction / 2;

            var spawnPoint = position + midPoint;

            connection.transform.position = spawnPoint;
            connection.transform.rotation = GetConnectorRotation(direction);
            connection.transform.localScale = new Vector3(0.2f,((direction.magnitude) / 2), 0.2f);
        });
    }

    Quaternion GetConnectorRotation(Vector3 direction)
    {
        return Quaternion.FromToRotation(Vector3.up, direction);
    }
}
