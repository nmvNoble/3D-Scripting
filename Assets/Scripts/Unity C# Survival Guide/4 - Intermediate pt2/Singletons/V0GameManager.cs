using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0GameManager : V0MonoSingleton<V0GameManager>
{
    public override void Init()
    {
        base.Init();
    }

    public Wizard wizard;
    private void OnEnable()
    {
        Player.onDeath += ResetPlayer;
    }

    public void ResetPlayer()
    {
        Debug.Log("Resetting Player");
        wizard.ResetWizard();
    }
}
