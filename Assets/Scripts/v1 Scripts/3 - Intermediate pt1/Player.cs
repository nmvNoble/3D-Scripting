using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void onDeath();
    public static event onDeath OnDeath;

    public Wizard wizard;

    // Start is called before the first frame update
    void Start()
    {
        //RemoveEventListener();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EventMain.onTeleport += Spawn;
        if (wizard.Health <= 0 && wizard.level > 0)
            Death();
    }
    public static void RemoveEventListener()
    {
        foreach (Delegate d in OnDeath.GetInvocationList())
            if (d.Method.Name == "UpdatePlayerDeath")
                OnDeath -= (onDeath)d;
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
    }

    void Death()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
    }

    private void OnDisable()
    {
        EventMain.onTeleport -= Spawn;
    }
}
