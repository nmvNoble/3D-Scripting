using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{
    public string name;
    public int lvlRequired;
    public int expGained;

    public Spell(string name, int lvlRequired, int expGained)
    {
        this.name = name;
        this.lvlRequired = lvlRequired;
        this.expGained = expGained;
    }

    public int Cast()
    {
        Debug.Log("Casting: " + this.name);
        return this.expGained;
    }
}
