using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy playerInstance;
    void Start()
    {
        Debug.Log("Don't Destroy: " + this.name);
        DontDestroyOnLoad(transform.root.gameObject);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
