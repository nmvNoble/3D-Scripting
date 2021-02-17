using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public static Action OnGameOver;
    public Wizard wizard;
    public bool isGameOver, isPlayerResetting;

    public override void Init()
    {
        base.Init();
    }
    private void Start()
    {
        Time.timeScale = 1;
        isGameOver = false;
        isPlayerResetting = false;
    }

    private void Update()
    {
        if (wizard.Health <= 0 && wizard.level > 0)
        {
            ResetPlayer();
        }

        if (wizard.level <= 0)
        {
            wizard.DisplayStats();
            GameOver();
        }
    }

    private void OnEnable()
    {
        Player.OnDeath += ResetPlayer;
    }

    public void ResetPlayer()
    {
        if (!isGameOver)
        {
            Debug.Log("The Wizard has fallen!");
            wizard.gameObject.SetActive(false);
            //Debug.Log("Resetting Player");
            wizard.ResetWizard();
        }
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
        if (OnGameOver != null)
        {
            OnGameOver();
        }

    }

    public void ResetGame()
    {
        Debug.Log("Reset Game");
        UIManager.Instance.ResetGame();
        SpawnManager.Instance.ResetGame();
        wizard.ResetWizard();
        Time.timeScale = 1;
        isGameOver = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
