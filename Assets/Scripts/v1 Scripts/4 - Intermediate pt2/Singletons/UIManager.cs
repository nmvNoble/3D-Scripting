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

    public Text activeEnemiesText;
    public Text playerDeathsText;
    public Text playerHealthText;
    public Text playerLevelText;
    public Text wizardSpellText;
    public GameObject gameOverMenu;
    private int _playerDeaths = 0;

    public void OnEnable()
    {
        Wizard.OnDamage += UpdatePlayerHealth;
        Wizard.OnLvlUp += UpdatePlayerLevel;
        Wizard.OnCast += UpdateWizardSpell;
        Player.onDeath += UpdatePlayerDeath;
        GameManager.OnGameOver += GameOverMenu;
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

    public void UpdatePlayerHealth(int Health)
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

    public void GameOverMenu()
    {
        gameOverMenu.gameObject.SetActive(true);
    }
}
