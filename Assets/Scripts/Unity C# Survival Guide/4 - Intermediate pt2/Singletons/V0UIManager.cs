using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V0UIManager : V0MonoSingleton<V0UIManager>
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
    private int _playerDeaths = 0;

    public void OnEnable()
    {
        V0Wizard.OnDamage += UpdatePlayerHealth;
        V0Wizard.OnLvlUp += UpdatePlayerLevel;
        V0Player.onDeath += UpdatePlayerDeath;
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
        Debug.Log(spell);
        playerLevelText.text = "Level: " + lvl;
        wizardSpellText.text = "Current Spell\n"+spell;
}
}
