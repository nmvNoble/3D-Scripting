using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityHelper
{
    public static void SetPosToZero(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
    }

    public static void ChangeColor(GameObject obj, Color color, bool rand=false)
    {
        obj.GetComponent<MeshRenderer>().material.color = color;
        if (rand == true)
        {
            obj.GetComponent<MeshRenderer>().material.color =
                    new Color(Random.value, Random.value, Random.value);
        }
        
    }
}
