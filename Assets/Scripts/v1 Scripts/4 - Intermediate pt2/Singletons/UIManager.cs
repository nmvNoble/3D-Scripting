using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public override void Init()
    {
        base.Init();
    }

    public Text waveCountText;
    public Text activeEnemiesText;
    public Text playerDeathsText;
    public Text playerHealthText;
    public Text playerLevelText;
    public Text wizardSpellText;
    public GameObject gameStartMenu, gameOverMenu, waveEndMenu;
    public GameObject introMenu, iMenuBackBtn, iMenuStartBtn, iMenuHealth, iMenuControls, iMenuWaves;
    private int _playerDeaths = 0;
    private void Start()
    {
        //ToggleIntroMenu(true);
        gameOverMenu.gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        Wizard.OnDamage += UpdatePlayerHealth;
        Wizard.OnLvlUp += UpdatePlayerLevel;
        Wizard.OnCast += UpdateWizardSpell;
        Player.OnDeath += UpdatePlayerDeath;
        GameManager.OnGameOver += EnableGameOverMenu;
        GameManager.OnWaveStatusChange += ToggleWaveMenu;
    }

    public void UpdateWave(string waveCount)
    {
        waveCountText.text = "Wave: " + waveCount;
    }

    public void UpdateEnemyCount()
    {
        activeEnemiesText.text = "Active Enemies: " + SpawnManager.enemyCount;
    }

    public void UpdatePlayerDeath()
    {
        _playerDeaths++;
        playerDeathsText.text = "Player Deaths: " + _playerDeaths;
    }

    public void UpdatePlayerHealth(float Health)
    {
        playerHealthText.text = "Health: " + Health;
    }

    public void UpdatePlayerLevel(int lvl, string spell)
    {
        playerLevelText.text = "Level: " + lvl;
        wizardSpellText.text = "Current Spell\n" + spell;
    }

    public void UpdateWizardSpell(string spell)
    {
        wizardSpellText.text = "Current Spell\n" + spell;
    }

    public void DisableGameStartMenu()
    {
        gameStartMenu.gameObject.SetActive(false);
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.gameObject.SetActive(true);
    }

    public void ToggleWaveMenu(bool menuStatus)
    {
        waveEndMenu.gameObject.SetActive(menuStatus);
    }

    public void ToggleIntroMenu(bool menuStatus)
    {
        introMenu.gameObject.SetActive(menuStatus);
        //iMenuBackBtn.gameObject.SetActive(!GameManager.Instance.isFirstRun);
        //iMenuStartBtn.gameObject.SetActive(GameManager.Instance.isFirstRun);
    }

    public void ToggleIMenuText(GameObject iMenuText)
    {
        if (iMenuText.name == "Health Intro")
        {
            iMenuHealth.gameObject.SetActive(true);
            iMenuControls.gameObject.SetActive(false);
            iMenuWaves.gameObject.SetActive(false);
        }
        else if (iMenuText.name == "Controls Intro")
        {
            iMenuHealth.gameObject.SetActive(false);
            iMenuControls.gameObject.SetActive(true);
            iMenuWaves.gameObject.SetActive(false);
        }
        else if (iMenuText.name == "Waves Intro")
        {
            iMenuHealth.gameObject.SetActive(false);
            iMenuControls.gameObject.SetActive(false);
            iMenuWaves.gameObject.SetActive(true);
        }
    }

    public void ResetGame()
    {
        _playerDeaths = 0;
        gameOverMenu.gameObject.SetActive(false);
    }
}
