using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rune : Item
{
    public int spellStat;
    public float runeEffect;

    public Rune(string name, int id, string description, 
            int spellStat, float runeEffect)
            :base(name, id, description)
    {
        this.name = name;
        this.id = id;
        this.description = description;
        this.itemType = ItemType.Rune;
        this.spellStat = spellStat;
        this.runeEffect = runeEffect;
    }
}
