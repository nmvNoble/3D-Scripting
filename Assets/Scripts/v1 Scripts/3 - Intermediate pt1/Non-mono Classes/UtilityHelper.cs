using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityHelper
{

    public static void SetPosToZero(GameObject obj)
    {
        obj.transform.position = Vector3.zero;
    }

    public static void ChangeColor(GameObject obj, Color color, bool rand = false)
    {
        obj.GetComponent<MeshRenderer>().material.color = color;
        if (rand == true)
        {
            obj.GetComponent<MeshRenderer>().material.color =
                    new Color(Random.value, Random.value, Random.value);
        }

    }

    public static float GetElementMod(Color target, Color spell, bool rand = false)
    {
        Debug.Log("Elements. T: " + target.ToString() + ", S: " + spell);
        if (target.ToString() == "RGBA(1.000, 0.000, 0.000, 1.000)")
        {
            if (spell.ToString() == "RGBA(1.000, 0.000, 0.000, 0.000)")
            {
                Debug.Log("R-R Modifier: 1");
                return 1f;
            }
            else if (spell.ToString() == "Green")
            {
                return 0.5f;
            }
            else if (spell.ToString() == "Blue")
            {
                return 1.5f;
            }
            else
            {
                Debug.Log("Target has no Element!");
                return 0f;
            }
        }
        else if (target.ToString() == "Green")
        {
            if (spell.ToString() == "Red")
            {
                return 1.5f;
            }
            else if (spell.ToString() == "Green")
            {
                return 1f;
            }
            else if (spell.ToString() == "Blue")
            {
                return .5f;
            }
            else
            {
                Debug.Log("Target has no Element!");
                return 0f;
            }
        }
        else if (target.ToString() == "Blue")
        {
            if (spell.ToString() == "Red")
            {
                return .5f;
            }
            else if (spell.ToString() == "Green")
            {
                return 1.5f;
            }
            else if (spell.ToString() == "Blue")
            {
                return 1f;
            }
            else
            {
                Debug.Log("Target has no Element!");
                return 0f;
            }
        }
        else
        {
            Debug.Log("Target has no Element!");
            return 0f;
        }
    }
}
