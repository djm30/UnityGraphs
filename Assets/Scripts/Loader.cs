using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Loader : MonoBehaviour
{

    [SerializeField] private string FileName;
    [SerializeField] private GameObject nodePrefab;
    
    private List<GameObject> nodes = new List<GameObject>();
    private Dictionary<int, List<int>> destinations = new();

    private void Awake()
    {
        ReadFile();
        SpawnNodes();
        ConfigureNodes();
    }
    
    #region OnLoadFunctions
    private void ReadFile()
    {
        // Reading graph in from the file
        string path = Path.Combine(Application.dataPath + "/Graphs/" + FileName);
        if (!File.Exists(path))
        {
            Debug.Log("File not found");
            return;
        }
        var lines = File.ReadAllLines(path);
        lines.ToList().ForEach(line =>
        {
            var nums = line.Trim().Split(" ").Select(num => Convert.ToInt32(num)).ToArray();
            if (nums.Length == 1)
                destinations.TryAdd(nums[0], new List<int>());
            else
            {
                if(!destinations.TryAdd(nums.First(), nums.Skip(1).ToList()))
                    Debug.LogFormat("{0} has already been added to the dictionary", nums.First());
            }
        });
    }

    private void SpawnNodes()
    {
        // Instantiating all of the graph nodes
        for (int i = 0; i < destinations.Count; i++)
        {
            var nodeGameObject = Instantiate(nodePrefab, RandomSpawnLocation(), Quaternion.identity);
            nodes.Add(nodeGameObject);
        }
    }

    private void ConfigureNodes()
    {
        // Adding connected nodes to the nodes
        for (int i = 0; i < nodes.Count; i++)
        {
            var nodeGameObject = nodes[i];
            var node = nodeGameObject.GetComponent<Node>();

            node.ID = i;

            node.connectedNodes = new List<GameObject>();
            // Getting the list of IDs of destination nodes for the current node
            destinations[node.ID].ForEach(destinationNode =>
            {
                // Fetching the reference to the node object that corresponds with the ID
                node.connectedNodes.Add(nodes[destinationNode]);
            });
        }
    }

    private Vector3 RandomSpawnLocation()
    {

        float range = Mathf.Ceil(Mathf.Pow(nodes.Count, 1 / 3f)) + 50;
        float x, y, z;
        x = Random.Range(-range, range);
        y = Random.Range(-range, range);
        z = Random.Range(-range, range);
        return new Vector3(x, y, z);
    }
    #endregion
}
