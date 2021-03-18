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

    public Text waveCountText, activeEnemiesText;
    public Text playerDeathsText, playerHealthText, playerLevelText, playerExpText, wizardSpellText;
    public Text runeTitle, runeDesc, runeStatMod, runeStatMul;
    public GameObject gameStartMenu, gameOverMenu, waveEndMenu, runeMenu;
    public GameObject introMenu, iMenuHealth, iMenuElements, iMenuControls, iMenuSpellInfo, iMenuRunes, iMenuWaves;
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
        Wizard.OnDeath += UpdatePlayerDeath;
        GameManager.OnGameOver += EnableGameOverMenu;
        GameManager.OnWaveStatusChange += ToggleWaveMenu;
        GameManager.OnRuneChange += ToggleRuneText;
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
        playerDeathsText.text = "Player\nDeaths\n" + _playerDeaths;
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

    public void UpdatePlayerExp(int exp, int expCap)
    {
        playerExpText.text = "Exp: " + exp + "/" + expCap;
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

    public void ToggleRuneText(bool menuStatus, Rune rune=null)
    {
        ToggleRuneMenu(menuStatus);
        if(rune != null)
        {
            runeTitle.text = "Your Rune changes form...\n-Rune of " + rune.name+"-";
            runeDesc.text = "\"" + rune.description + "\"";
            runeStatMul.text = rune.runeEffect.ToString();
            if(rune.spellStat == 0)
                runeStatMod.text = "Experience";
            else if(rune.spellStat == 1)
                runeStatMod.text = "Cooldown";
            else if(rune.spellStat == 2)
                runeStatMod.text = "Damage";
            else if(rune.spellStat == 3)
                runeStatMod.text = "Diameter";
        }
    }

    public void ToggleRuneMenu(bool menuStatus)
    {
        runeMenu.gameObject.SetActive(menuStatus);
    }

    public void ToggleIntroMenu(bool menuStatus)
    {
        introMenu.gameObject.SetActive(menuStatus);
    }

    public void ToggleIMenuText(GameObject iMenuText)
    {
        if (iMenuText.name == "Health Intro")
        {
            iMenuHealth.gameObject.SetActive(true);
            iMenuElements.gameObject.SetActive(false);
            iMenuControls.gameObject.SetActive(false);
            iMenuSpellInfo.gameObject.SetActive(false);
            iMenuRunes.gameObject.SetActive(false);
            iMenuWaves.gameObject.SetActive(false);
        }
        else if (iMenuText.name == "Elements Intro")
        {
            iMenuHealth.gameObject.SetActive(false);
            iMenuElements.gameObject.SetActive(true);
            iMenuControls.gameObject.SetActive(false);
            iMenuSpellInfo.gameObject.SetActive(false);
            iMenuRunes.gameObject.SetActive(false);
            iMenuWaves.gameObject.SetActive(false);
        }
        else if (iMenuText.name == "Controls Intro")
        {
            iMenuHealth.gameObject.SetActive(false);
            iMenuElements.gameObject.SetActive(false);
            iMenuControls.gameObject.SetActive(true);
            iMenuSpellInfo.gameObject.SetActive(false);
            iMenuRunes.gameObject.SetActive(false);
            iMenuWaves.gameObject.SetActive(false);
        }
        else if (iMenuText.name == "Spell Info Intro")
        {
            iMenuHealth.gameObject.SetActive(false);
            iMenuElements.gameObject.SetActive(false);
            iMenuControls.gameObject.SetActive(false);
            iMenuSpellInfo.gameObject.SetActive(true);
            iMenuRunes.gameObject.SetActive(false);
            iMenuWaves.gameObject.SetActive(false);
        }
        else if (iMenuText.name == "Runes Intro")
        {
            iMenuHealth.gameObject.SetActive(false);
            iMenuElements.gameObject.SetActive(false);
            iMenuControls.gameObject.SetActive(false);
            iMenuSpellInfo.gameObject.SetActive(false);
            iMenuRunes.gameObject.SetActive(true);
            iMenuWaves.gameObject.SetActive(false);
        }
        else if (iMenuText.name == "Waves Intro")
        {
            iMenuHealth.gameObject.SetActive(false);
            iMenuElements.gameObject.SetActive(false);
            iMenuControls.gameObject.SetActive(false);
            iMenuSpellInfo.gameObject.SetActive(false);
            iMenuRunes.gameObject.SetActive(false);
            iMenuWaves.gameObject.SetActive(true);
        }
    }

    public void ResetGame()
    {
        _playerDeaths = -1;
        UpdatePlayerDeath();
        gameOverMenu.gameObject.SetActive(false);
    }
}
