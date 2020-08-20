using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventMain : MonoBehaviour
{
    public delegate void ActionClick();
    public static event ActionClick onClick;

    public delegate void Teleport(Vector3 pos);
    public static event Teleport onTeleport;

    public void ButtonClick()
    {
        if (onClick!=null)
        {
            onClick();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(onTeleport != null)
            {
                Vector3 pos = new Vector3(Random.Range(-4, -8),
                    Random.Range(3, 7), -3);
                onTeleport(pos);
            }
        }
    }
}
