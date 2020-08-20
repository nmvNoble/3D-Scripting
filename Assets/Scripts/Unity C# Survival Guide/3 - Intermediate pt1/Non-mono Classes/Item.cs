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

    public enum ItemType
    {
        Note,
        Rune,
        Consumable,
        Weapon
    }

    public ItemType itemType;

    public Item(string name, int id, string desc)
    {
        this.name = name;
        this.id = id;
        this.description = desc;
    }
    public Item(string name, int id, string desc, int type)
    {
        this.name = name;
        this.id = id;
        this.description = desc;
        this.itemType = (ItemType)type;
    }

    public void enumTyping()
    {
        switch (this.itemType)
        {
            case ItemType.Rune:
                Debug.Log("Rune Item: "+this.name);
                break;
            case ItemType.Consumable:
                Debug.Log("Consumable Item: " + this.name);
                break;
            case ItemType.Weapon:
                Debug.Log("Weapon Item: " + this.name);
                break;
        }
    }
}
