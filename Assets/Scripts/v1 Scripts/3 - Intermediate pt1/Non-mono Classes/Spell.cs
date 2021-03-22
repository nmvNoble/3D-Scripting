using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{
    public string name;
    public int lvlRequired;
    public int expGained;

    public float spellCD, spellDmgMod, spellDiameter;
    public Color spellColor;
    public Rune runeSlot = null;
    private Spell defaultSpellStats;

    public Spell(string name, int lvlRequired, int expGained)
    {
        this.name = name;
        this.lvlRequired = lvlRequired;
        this.expGained = expGained;
    }
    public Spell(Spell newSpell)
    {
        this.name = newSpell.name;
        this.lvlRequired = newSpell.lvlRequired;
        this.expGained = newSpell.expGained;
        this.spellCD = newSpell.spellCD;
        this.spellDmgMod = newSpell.spellDmgMod;
        this.spellDiameter = newSpell.spellDiameter;
        this.spellColor = newSpell.spellColor;
}

    public int Cast(Vector3 enemyPos)
    {
        //Debug.Log("Casting: " + this.name);
        //Debug.Log("Wizard gains " + expGained + " Exp");
        return 0;//this.expGained;
    }

    public void SetDefaultSpellStats()
    {
        defaultSpellStats = new Spell(this);
        //defaultSpellStats.expGained = expGained;
        //defaultSpellStats.spellCD = spellCD;
        //defaultSpellStats.spellDmgMod = spellDmgMod;
        //defaultSpellStats.spellDiameter = spellDiameter;
    }

    public void ApplyRune()
    {
        ResetSpellStats();
        if (runeSlot != null)
        {
            switch (runeSlot.spellStat)
            {
                case 0:
                    //Debug.Log("Stat before: " + expGained);
                    expGained = (int)((float)expGained * runeSlot.runeEffect);
                    //Debug.Log("Stat after: " + expGained);
                    break;
                case 1:
                    //Debug.Log("Stat before: " + spellCD);
                    spellCD *= runeSlot.runeEffect;
                    //Debug.Log("Stat after: " + spellCD);
                    break;
                case 2:
                    //Debug.Log("Stat before: " + spellDmgMod);
                    spellDmgMod += runeSlot.runeEffect;
                    //Debug.Log("Stat after: " + spellDmgMod);
                    break;
                case 3:
                    //Debug.Log("Stat before: " + spellDiameter);
                    spellDiameter *= runeSlot.runeEffect;
                    //Debug.Log("Stat after: " + spellDiameter);
                    break;
            }
        }
    }

    public void RemoveRune()
    {
        ResetSpellStats();
        runeSlot = null;
    }

    public void ResetSpellStats()
    {
        expGained = defaultSpellStats.expGained;
        spellCD = defaultSpellStats.spellCD;
        spellDmgMod = defaultSpellStats.spellDmgMod;
        spellDiameter = defaultSpellStats.spellDiameter;
    }
}
