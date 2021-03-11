using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0Duck : V0Pet
{
    protected override void Speak()
    {
        base.Speak();
        Debug.Log("Quack!");
    }
}
