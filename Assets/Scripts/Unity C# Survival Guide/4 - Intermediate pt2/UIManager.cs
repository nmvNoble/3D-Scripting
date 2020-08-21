using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text activeEnemiesText;
    public Text playerDeathsText;
    public Text playerHealthText;
    private int _playerDeaths = 0;

    public void OnEnable()
    {
        Wizard.OnDamage += UpdatePlayerHealth;
        Player.onDeath += UpdatePlayerDeath;
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
}
