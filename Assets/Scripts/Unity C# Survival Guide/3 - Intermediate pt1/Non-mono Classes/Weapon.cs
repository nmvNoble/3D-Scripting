using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Item
{
    public int damage;
    public int strBonus;
    public int dexBonus;

    public Weapon(string name, int id, string description, 
            int damage, int strBonus, int dexBonus)
            :base(name, id, description)
    {
        this.name = name;
        this.id = id;
        this.description = description;
        this.damage = damage;
        this.strBonus = strBonus;
        this.dexBonus = dexBonus;
    }
}
