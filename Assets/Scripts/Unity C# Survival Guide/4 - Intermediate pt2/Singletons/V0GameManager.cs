using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0GameManager : V0MonoSingleton<V0GameManager>
{
    public override void Init()
    {
        base.Init();
    }

    public V0Wizard wizard;
    private void OnEnable()
    {
        Player.OnDeath += ResetPlayer;
    }

    public void ResetPlayer()
    {
        Debug.Log("Resetting Player");
        wizard.ResetWizard();
    }
}
