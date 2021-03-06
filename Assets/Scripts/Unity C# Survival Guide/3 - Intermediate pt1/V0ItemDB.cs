﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class V0ItemDB : MonoBehaviour
{
    //public Item[] items;
    //public Consumable[] consumables;
    //public Weapon[] weapons;
    [SerializeField]
    private List<V0Item> _items;
    public List<V0Item> Items
    {
        get
        {
            return _items;
        }
        private set
        {
            _items = value;
        }
    }
    private Dictionary<int, V0Item> _runes;
    public Dictionary<int, V0Item> Runes
    {
        get
        {
            return _runes;
        }
        private set
        {
            _runes = value;
        }
    }

    public List<V0Consumable> consumables = new List<V0Consumable>();
    public List<V0Weapon> weapons = new List<V0Weapon>();


    [SerializeField]
    V0Item note1, note2;
    V0Item Rr, Ro, Rw, Rb;

    // Start is called before the first frame update
    void Start()
    {
        note1 = new V0Item("note 1", 0, "sword is Sharp boi.");
        note2 = CreateItem("note 2", 1, "staff is Bonky boi.");
        Rr = CreateItem("Rune-Red", 0, "A Warm Rune.", 1);
        Ro = CreateItem("Rune-Orange", 1, "A Hot Rune.", 1);
        Rw = CreateItem("Rune-White", 2, "A Scalding Rune.", 1);
        Rb = CreateItem("Rune-Blue", 3, "A Searing Rune.", 1);
        Runes = new Dictionary<int, V0Item>
        {
            { Rr.id, Rr },
            { Ro.id, Ro },
            { Rw.id, Rw },
            { Rb.id, Rb }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ItemTyping();
        }
    }

    public void ItemTyping()
    {
        //Debug.Log("Item Actions: ");
        //foreach (var item in Items)
        //{
        //    item.Action();
        //}
        Debug.Log("Item Typing: ");
        foreach (var val in Runes.Values)
        {
            val.enumTyping();
        }

        //Linq
        var highDex = weapons.Where(w => w.dexBonus >= 3);
        Debug.Log("Weapon Typing(Dex Bonus 3 and up): ");
        foreach (var weap in highDex)
        {
            weap.enumTyping();
            Debug.Log(weap.name + " Dmg(" + weap.damage + "), " +
                "StrB(" + weap.strBonus + "), DexB(" + weap.dexBonus + ")");
        }

    }

    private V0Item CreateItem(string name, int id, string desc)
    {
        var item = new V0Item(name, id, desc);
        return item;
    }
    private V0Item CreateItem(string name, int id, string desc, int type)
    {
        var item = new V0Item(name, id, desc, type);
        return item;
    }

    public void AddItem(int index, V0Wizard player)
    {
        foreach (var rune in _items)
        {
            if (index == rune.id)
            {
                if (rune != null && player.runes[index].name == rune.name)
                {
                    Debug.Log("Rune already aquired!");
                    return;
                } else
                player.runes[index] = rune;
                Debug.Log("Aquired " + rune.name);
            }
        }
    }
    public void RemoveItem(int index, Wizard player)
    {
        foreach (var rune in player.runes)
        {
            if (rune.name == _items[index].name)
            {
                player.runes[index] = null;
                Debug.Log("Removed " + rune.name);
            }
        }
    }

    public void AddRune(int index, V0Wizard player)
    {
        foreach (var rune in Runes)//_items)
        {
            if (index == rune.Key) //.id)
            {
                if (rune.Value != null && player.runes[index].name == rune.Value.name)
                {
                    Debug.Log("Rune already aquired!");
                    return;
                }
                else
                    player.runes[index] = rune.Value;
                Debug.Log("Aquired " + rune.Value.name);
            }
        }
    }
    public void RemoveRune(int index, V0Wizard player)
    {
        foreach (var rune in player.runes)
        {
            if (rune.name == Runes[index].name)
            {
                player.runes[index] = null;
                Debug.Log("Removed " + rune.name);
            }
        }
    }
}
