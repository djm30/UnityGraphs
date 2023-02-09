using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Loader : MonoBehaviour
{

    [FormerlySerializedAs("FileName")] [SerializeField] private string fileName;
    
    private readonly SparseMatrix _matrix = SparseMatrix.GetInstance();

    private void Awake()
    {
        ReadFile();
        // SpawnNodes();
        // ConfigureNodes();
    }
    
    private void ReadFile()
    {
        // Reading graph in from the file
        var path = Path.Combine(Application.dataPath + "/Graphs/" + fileName);
        if (!File.Exists(path))
        {
            Debug.Log("File not found");
            return;
        }
        var lines = File.ReadAllLines(path);
        lines.ToList().ForEach(line =>
        {
            var nums = line.Trim().Split(" ").Select(num => Convert.ToInt32(num)).ToArray();
            _matrix.AddNode(nums[0], nums.Length == 1 ? new List<int>() : nums.Skip(1).ToList());
        });
    }
}
