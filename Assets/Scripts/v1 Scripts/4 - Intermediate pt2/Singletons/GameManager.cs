using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public static Action OnGameOver;
    public static Action<bool> OnWaveStatusChange;
    public static Action<bool, Rune> OnRuneChange;
    public bool isGameOver, isPlayerResetting, isFirstRun;
    public int wave;

    [SerializeField]
    private Wizard wizard;
    private ItemDB _iDB;
    private int runeGained;
    private bool _isWaveOngoing, lastRune = false;

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
        if (wizard.level <= 0)
        {
            wizard.DisplayStats();
            GameOver();
        }
        if (_isWaveOngoing == true && wizard.exp >= wizard.expCap)
        {
            WaveStatusChange(_isWaveOngoing);
        }

    }

    private void OnEnable()
    {
        //Wizard.OnDeath += ResetPlayer;
    }

    public void ResetPlayer()
    {
        if (!isGameOver)
        {
            //Debug.Log("The Wizard has fallen at Lvl: " + wizard.level + "!");
            wizard.Die();
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
        if (ongoingWave)
        {
            _isWaveOngoing = false;
            Time.timeScale = 0;
            wizard.LevelUp();
            wave++;
            UIManager.Instance.UpdateWave(wave.ToString());
            SpawnManager.Instance.ResetEnemies();
            if (wave >= 4)
            {
                wizard.ResetRunes();
                runeGained = UnityEngine.Random.Range(1, 4);
                _iDB.AddRune(runeGained, wizard); //UnityEngine.Random.Range(0, 4), wizard);
                                                  //wizard.currSpell.runeSlot = wizard.runes[runeGained;
                                                  //wizard.currSpell.ApplyRune();
                OnRuneChange(ongoingWave, wizard.runes[runeGained]);
                if (lastRune)
                    UIManager.Instance.ToggleRuneMenu(false);
            }
        }
        else
        {
            _isWaveOngoing = true;
            Time.timeScale = 1;
        }
    }

    public void PlayerGainRune(int runeSpellType)
    {
        wizard.ApplyRune(runeGained, runeSpellType);
            UIManager.Instance.ToggleRuneMenu(false);
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
        SpawnManager.Instance.ResetEnemies();
        wizard.ResetWizard();
        Time.timeScale = 1;
        isGameOver = false;
        wave = 1;
        UIManager.Instance.UpdateWave(wave.ToString());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
