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

    [SerializeField]
    private Text activeEnemiesText, pauseBtn, timeText, waveCountText,
        runeTitle, runeDesc, runeStatMod, runeStatMul, 
        wizardDeathsText, wizardExpText, wizardHealthText, wizardLevelText, wizardSpellText;

    [SerializeField]
    private GameObject gameStartMenu, gameOverMenu, pauseMenu, runeMenu, waveEndMenu, 
        introMenu, iMenuControls, iMenuElements, iMenuEnemies, iMenuHealth, iMenuRunes, iMenuSpellInfo, iMenuWaves;
    
    private int wizardDeaths = 0;

    private void Start()
    {
        gameStartMenu.SetActive(true);
        gameOverMenu.SetActive(false);
    }
    public void OnEnable()
    {
        Wizard.OnDamage += UpdateWizardHealth;
        Wizard.OnLvlUp += UpdateWizardLevel;
        Wizard.OnCast += UpdateWizardSpell;
        Wizard.OnDeath += UpdateWizardDeath;
        GameManager.OnGameOver += EnableGameOverMenu;
        GameManager.OnWaveStatusChange += ToggleWaveMenu;
        GameManager.OnRuneChange += ToggleRuneText;
        GameManager.OnToggleTime += TogglePauseMenu;
    }

    public void UpdateWave(string waveCount)
    {
        waveCountText.text = "Wave: " + waveCount;
    }

    public void UpdateWizardDeath()
    {
        wizardDeaths++;
        wizardDeathsText.text = "Wizard\nDeaths\n" + wizardDeaths;
    }

    public void UpdateWizardHealth(float Health)
    {
        wizardHealthText.text = "Health: " + Health;
    }

    public void UpdateWizardLevel(int lvl, string spell)
    {
        wizardLevelText.text = "Level: " + lvl;
        wizardSpellText.text = "Current Spell\n" + spell;
    }

    public void UpdateWizardExp(int exp, int expCap)
    {
        wizardExpText.text = "Exp: " + exp + "/" + expCap;
    }

    public void UpdateWizardSpell(string spell)
    {
        wizardSpellText.text = "Current Spell\n" + spell;
    }

    public void UpdateTimer(int timeMin, int timeSec)
    {
        timeText.text = timeMin + ":" + timeSec;
    }

    public void DisableGameStartMenu()
    {
        gameStartMenu.gameObject.SetActive(false);
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.gameObject.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        if (Time.timeScale == 1)
        {
            pauseMenu.SetActive(true);
            pauseBtn.text = "Resume";
        }
        else if (Time.timeScale == 0)
        {
            pauseMenu.SetActive(false);
            pauseBtn.text = "Pause";
        }
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
            iMenuHealth.gameObject.SetActive(true);
        else
            iMenuHealth.gameObject.SetActive(false);

        if (iMenuText.name == "Elements Intro")
            iMenuElements.gameObject.SetActive(true);
        else
            iMenuElements.gameObject.SetActive(false); 
        
        if (iMenuText.name == "Enemies Intro")
            iMenuEnemies.gameObject.SetActive(true);
        else
            iMenuEnemies.gameObject.SetActive(false);
        
        if (iMenuText.name == "Controls Intro")
            iMenuControls.gameObject.SetActive(true);
        else
            iMenuControls.gameObject.SetActive(false);

        if (iMenuText.name == "Spell Info Intro")
            iMenuSpellInfo.gameObject.SetActive(true);
        else
            iMenuSpellInfo.gameObject.SetActive(false);
        
        if (iMenuText.name == "Runes Intro")
            iMenuRunes.gameObject.SetActive(true);
        else
            iMenuRunes.gameObject.SetActive(false);
        
        if (iMenuText.name == "Waves Intro")
            iMenuWaves.gameObject.SetActive(true);
        else
            iMenuWaves.gameObject.SetActive(false);

    }

    public void ResetGame()
    {
        wizardDeaths = -1;
        UpdateWizardDeath();
        gameOverMenu.gameObject.SetActive(false);
        pauseMenu.SetActive(false);
        pauseBtn.text = "Pause";
    }
}
