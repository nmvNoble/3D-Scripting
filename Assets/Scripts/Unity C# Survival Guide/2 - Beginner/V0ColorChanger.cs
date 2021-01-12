using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0ColorChanger : MonoBehaviour
{
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        objects = GetAllObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject[] GetAllObjects()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Objects");

        foreach (var p in allObjects)
        {
            p.GetComponent<MeshRenderer>().material.color =
                    new Color(Random.value, Random.value, Random.value);
        }

        return allObjects;
    }
}
