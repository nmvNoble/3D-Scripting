using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class V0MonoSingleton<T> : MonoBehaviour where T : V0MonoSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError(typeof(T).ToString() + " is NULL.");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = (T)this; //this as T;
        Init();
    }

    public virtual void Init()
    {
        //optional
    }
}
