using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    private ItemDB _iDB;
    public static Action OnGameOver;
    public static Action<bool> OnWaveStatusChange;
    public static Action<bool, Rune> OnRuneChange;
    [SerializeField]
     Wizard wizard;
    public bool isGameOver, isPlayerResetting, isFirstRun;
    public int wave;
    private bool _isWaveOngoing;

    public override void Init()
    {
        base.Init();
    }
    private void Start()
    {
        _iDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        Time.timeScale = 0;
        isFirstRun = true;
    }

    private void Update()
    {
        if (wizard.Health <= 0 && wizard.level > 0)
        {
            ResetPlayer();
        }
        if (_isWaveOngoing == true && wizard.exp >= wizard.expCap)
        {
            WaveStatusChange(_isWaveOngoing);
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
            Debug.Log("The Wizard has fallen at Lvl: " + wizard.level + "!");
            wizard.gameObject.SetActive(false);
            //Debug.Log("Resetting Player");
            wizard.ResetWizard();
        }
    }

    public void WaveStatusChange(bool ongoingWave)
    {
        if (OnWaveStatusChange != null)
        {
            OnWaveStatusChange(ongoingWave);
        }
        if (OnWaveStatusChange != null && wave+1 >= 2)
        {
            _iDB.AddRune(1, wizard); //UnityEngine.Random.Range(0, 4), wizard);
            wizard.currSpell.runeSlot = wizard.runes[1];
            wizard.currSpell.ApplyRune();
            OnRuneChange(ongoingWave, wizard.runes[1]);
        }
        if (ongoingWave)
        {
            _isWaveOngoing = false;
            Time.timeScale = 0;
            wizard.LevelUp();
            wave++;
            UIManager.Instance.UpdateWave(wave.ToString());
            SpawnManager.Instance.ResetBandits();
        }
        else
        {
            _isWaveOngoing = true;
            Time.timeScale = 1;
        }
    }

    public void WaveRuneChange(bool ongoingWave)
    {
        if (ongoingWave)
        {
            _isWaveOngoing = false;
            Time.timeScale = 0;
            wizard.LevelUp();
            wave++;
            UIManager.Instance.UpdateWave(wave.ToString());
            SpawnManager.Instance.ResetBandits();
        }
        else
        {
            _isWaveOngoing = true;
            Time.timeScale = 1;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        if (isFirstRun)
        {
            isFirstRun = false;
            UIManager.Instance.DisableGameStartMenu();
        }

        isGameOver = false;
        isPlayerResetting = false;

        wave = 1;
        _isWaveOngoing = true;
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
        SpawnManager.Instance.ResetBandits();
        wizard.ResetWizard();
        Time.timeScale = 1;
        isGameOver = false;
        wave = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
