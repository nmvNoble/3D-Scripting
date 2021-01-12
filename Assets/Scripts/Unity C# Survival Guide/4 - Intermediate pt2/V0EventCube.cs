using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0EventCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        V0EventMain.onClick += TurnMagenta;
    }

    public void TurnMagenta()
    {
        GetComponent<MeshRenderer>().material.color = Color.magenta;
    }

    private void OnDisable()
    {
        V0EventMain.onClick -= TurnMagenta;
    }
}
