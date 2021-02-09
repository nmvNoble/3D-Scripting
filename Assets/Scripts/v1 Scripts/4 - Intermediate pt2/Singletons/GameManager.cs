using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public static Action OnGameOver;
    public Wizard wizard;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (wizard.level <= 0)
            GameOver();
    }

    private void OnEnable()
    {
        Player.onDeath += ResetPlayer;
    }

    public void ResetPlayer()
    {
        Debug.Log("Resetting Player");
        wizard.ResetWizard();
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        Time.timeScale = 0;
        if (OnGameOver != null)
        {
            OnGameOver();
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
