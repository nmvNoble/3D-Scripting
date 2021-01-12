using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0Player : MonoBehaviour
{
    public delegate void OnDeath();
    public static event OnDeath onDeath;
    public bool isWizDead = false;

    public V0Wizard wizard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EventMain.onTeleport += Spawn;
        if (wizard.Health <= 0 && !isWizDead)
            Death();
    }

    public void Spawn(Vector3 pos)
    {
        transform.position = pos;
    }

    void Death()
    {
        isWizDead = true;
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
