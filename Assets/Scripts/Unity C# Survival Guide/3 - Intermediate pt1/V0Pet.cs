using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0Pet : MonoBehaviour
{
    [SerializeField]
    protected string petName;

    private void Start()
    {
        Speak();
    }

    protected virtual void Speak()
    {
        Debug.Log(petName + " says: ");
    }
}
