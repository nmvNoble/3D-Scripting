using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public Item[] items;
    public Consumable[] consumables;
    public Weapon[] weapons;
    [SerializeField]
    Item note1, note2;

    // Start is called before the first frame update
    void Start()
    {
        note1 = new Item("note 1", 0, "sword is Sharp boi.");
        note2 = CreateItem("note 2", 1, "staff is Bonky boi.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Item CreateItem(string name, int id, string desc)
    {
        var sword = new Item(name, id, desc);
        return sword;
    }
}
