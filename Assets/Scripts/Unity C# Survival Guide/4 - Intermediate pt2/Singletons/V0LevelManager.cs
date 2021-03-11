using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0LevelManager : MonoBehaviour
{
    private static V0LevelManager _instance;
    public static V0LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("LevelManager"); //Lazy Instantiate
                go.AddComponent<V0LevelManager>();
            }
            return _instance;
        }
    }

    public void Awake()
    {
        _instance = this;
    }
}
