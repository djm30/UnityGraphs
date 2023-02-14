using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class OrgNode : MonoBehaviour
{
    [SerializeField] public string name;
    [SerializeField] public List<OrgNode> connectedNodes;
    [SerializeField] public OrgNode parentNode;
    [SerializeField] public int weight;
    private float scale = 2f;

    private void Awake()
    {
        if (parentNode is null) return;
        parentNode.connectedNodes.Add(this);
    }

    private void Start()
    {
        if(parentNode is null)
            PositionChildNodes();
    }

    public void PositionChildNodes()
    {
        Debug.Log("Positioning child nodes");
        connectedNodes.ForEach(node =>
        {
            // Set node position to be below this node with a magnitude of the weight
            // Calculate a random offset based on the weight
            Vector3 randomOffset = Random.onUnitSphere * node.weight;

            // Set the position of the child node below the parent node
            Vector3 position = transform.position - new Vector3(0, node.weight, 0) + randomOffset;
            node.transform.position = position * scale;
            
            // Then position the child nodes of the child node
            node.PositionChildNodes();
        });
    }
}
