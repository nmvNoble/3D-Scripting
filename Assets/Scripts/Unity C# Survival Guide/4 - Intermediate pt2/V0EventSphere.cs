using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0EventSphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        V0EventMain.onClick += Fall;
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
        V0EventMain.onClick -= Fall;
    }
}
