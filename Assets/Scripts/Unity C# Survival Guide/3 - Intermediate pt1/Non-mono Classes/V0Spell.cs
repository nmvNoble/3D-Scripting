using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class V0Spell
{
    public string name;
    public int lvlRequired;
    public int expGained;

    public V0Spell(string name, int lvlRequired, int expGained)
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
