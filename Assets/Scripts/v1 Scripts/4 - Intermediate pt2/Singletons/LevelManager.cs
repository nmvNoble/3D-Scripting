using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("LevelManager"); //Lazy Instantiate
                go.AddComponent<LevelManager>();
            }
            return _instance;
        }
    }

    public void Awake()
    {
        _instance = this;
    }
}
