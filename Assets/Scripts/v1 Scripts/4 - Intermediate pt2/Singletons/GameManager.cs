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

    public bool IsGameOver { get { return isGameOver; } }
    public int Wave { get { return wave; } }

    private Wizard wizard;
    private ItemDB iDB;
    private int runeGained, timeSec, timeMin, wave;
    private bool isGameOver, isWaveOngoing;
    private GameObject destroySpell;

    public override void Init() {
        base.Init();
    }

    private void Start()
    {
        wizard = GameObject.Find("Wizard").GetComponent<Wizard>();
        iDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        Time.timeScale = 0;
        timeSec = 0;
        timeMin = 0;
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (wizard.Health <= 0 && wizard.Level > 0)
            ResetPlayer();

        if (wizard.Level <= 0)
        {
            wizard.DisplayStats();
            GameOver();
        }

        else if (isWaveOngoing == true && wizard.Exp >= wizard.ExpCap)
        {
            wizard.LevelUp();
            if(wizard.Level >= wave)//wizard.level != 1 && wizard.level >= wave)
                WaveStatusChange(isWaveOngoing);
        }
    }

    public IEnumerator StartTimer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1.0f);
            timeSec++;
            if (timeSec >= 60)
            {
                timeMin++;
                timeSec = 0;
            }
            UIManager.Instance.UpdateTimer(timeMin, timeSec);
        }
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
        OnWaveStatusChange?.Invoke(ongoingWave);

        if (ongoingWave)
        {
            isWaveOngoing = false;
            Time.timeScale = 0;
            wave++;
            UIManager.Instance.UpdateWave(wave.ToString());
            SpawnManager.Instance.ResetEnemies();
            if (wave >= 4)
            {
                wizard.ResetRunes();
                runeGained = UnityEngine.Random.Range(1, 4);
                iDB.AddRune(runeGained, wizard); 
                OnRuneChange(ongoingWave, wizard.runes[runeGained]);
            }
        }
        else
        {
            isWaveOngoing = true;
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
        UIManager.Instance.DisableGameStartMenu();
        isGameOver = false;
        wave = 1;
        isWaveOngoing = true;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
        CheckExistingSpellEffects();
        OnGameOver?.Invoke();
    }

    public void CheckExistingSpellEffects()
    {
        destroySpell = GameObject.Find("Sphere");
        if (destroySpell != null)
        {
            Debug.Log("Destroy left-over SpellEffect");
            Destroy(destroySpell);
            destroySpell = null;
        }
    }

    public void ResetGame()
    {
        Debug.Log("Reset Game");
        UIManager.Instance.ResetGame();
        SpawnManager.Instance.ResetEnemies();
        wizard.ResetWizard();
        isGameOver = false;
        timeSec = 0;
        timeMin = 0;
        wave = 1;
        UIManager.Instance.UpdateWave(wave.ToString());
        Time.timeScale = 1;
    }
}
