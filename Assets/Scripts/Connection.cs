using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{

    [SerializeField] private GameObject otherNode;
    [SerializeField] private GameObject connector;
    private SpringJoint spring;
    
    // Start is called before the first frame update
    void Start()
    {
        var direction = otherNode.transform.position - transform.position;
        var midPoint = direction / 2;
        var cylinder = Instantiate(connector, midPoint, Quaternion.FromToRotation(Vector3.up, direction));
        cylinder.transform.localScale = new Vector3(0.2f,((direction.magnitude) / 2), 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
