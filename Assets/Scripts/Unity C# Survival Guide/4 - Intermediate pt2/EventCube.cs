using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventMain.onClick += TurnMagenta;
    }

    public void TurnMagenta()
    {
        GetComponent<MeshRenderer>().material.color = Color.magenta;
    }

    private void OnDisable()
    {
        EventMain.onClick -= TurnMagenta;
    }
}
