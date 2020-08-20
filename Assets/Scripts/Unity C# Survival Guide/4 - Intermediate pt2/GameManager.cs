using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
