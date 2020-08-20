using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventMain.onClick += Fall;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fall()
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnDisable()
    {
        EventMain.onClick -= Fall;
    }
}
