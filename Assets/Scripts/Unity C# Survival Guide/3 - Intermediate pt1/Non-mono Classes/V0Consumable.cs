using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class V0Consumable : Item
{
    public int hpEffect;
    public string statusName;
    public bool statusEffect;

    public V0Consumable(string name, int id, string description, 
            int hpEffect, string statusName, bool statusEffect)
            :base(name, id, description)
    {
        this.name = name;
        this.id = id;
        this.description = description;
        this.hpEffect = hpEffect;
        this.statusName = statusName;
        this.statusEffect = statusEffect;
    }
}
