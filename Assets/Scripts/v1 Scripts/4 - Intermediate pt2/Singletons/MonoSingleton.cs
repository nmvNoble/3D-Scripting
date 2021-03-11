using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;
    private static MonoSingleton<T> _MSTinstance;
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
        DontDestroyOnLoad(this.gameObject);

        if (_MSTinstance == null)
        {
            _MSTinstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
