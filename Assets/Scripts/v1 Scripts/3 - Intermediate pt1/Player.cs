using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void OnDeath();
    public static event OnDeath onDeath;

    public Wizard wizard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EventMain.onTeleport += Spawn;
        if (wizard.Health <= 0)
            Death();
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
    }

    void Death()
    {
        if (onDeath != null)
        {
            onDeath();
        }
    }

    private void OnDisable()
    {
        EventMain.onTeleport -= Spawn;
    }
}
