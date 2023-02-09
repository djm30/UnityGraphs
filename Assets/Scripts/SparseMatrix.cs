using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class SparseMatrix
    {

        private static SparseMatrix _instance;
        
        private readonly Dictionary<int, List<int>> _destinations;

        private SparseMatrix()
        {
            _destinations = new Dictionary<int, List<int>>();
        }
        
        
        public void AddNode(int source, List<int> sourceDestinations)
        {
            if (!_destinations.TryAdd(source, sourceDestinations))
            {
                Debug.LogFormat("Node with ID: {0} has already been added to the graph", source);
            }
        }

        public List<int> GetNode(int source)
        {
            return _destinations[source];
        }

        public int NumNodes()
        {
            return _destinations.Count;
        }

        public static SparseMatrix GetInstance()
        {
            if (_instance is null)
                _instance = new SparseMatrix();
            return _instance;
        }
    }
}