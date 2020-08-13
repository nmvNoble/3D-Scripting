using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public string description;
    public Sprite icon;

    public Item(string name, int id, string desc)
    {
        this.name = name;
        this.id = id;
        this.description = desc;
    }
}
