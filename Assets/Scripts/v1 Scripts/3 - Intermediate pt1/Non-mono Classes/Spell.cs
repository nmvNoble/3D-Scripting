using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{
    public string name;
    public int lvlRequired;
    public int expGained;

    public float spellCD;
    public int spellDmg, spellRadius;
    public Color spellColor;

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
        this.spellDmg = newSpell.spellDmg;
        this.spellRadius = newSpell.spellRadius;
        this.spellColor = newSpell.spellColor;
}

    public int Cast(Vector3 enemyPos)
    {
        //Debug.Log("Casting: " + this.name);
        //Debug.Log("Wizard gains " + expGained + " Exp");
        return this.expGained;
    }
}
