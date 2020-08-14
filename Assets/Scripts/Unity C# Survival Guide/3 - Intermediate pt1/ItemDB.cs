using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    //public Item[] items;
    //public Consumable[] consumables;
    //public Weapon[] weapons;
    [SerializeField]
    private List<Item> _items; 
    public List<Item> Items
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
    public List<Consumable> consumables = new List<Consumable>();
    public List<Weapon> weapons = new List<Weapon>();


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
        var item = new Item(name, id, desc);
        return item;
    }

    public void AddItem(int index, Wizard player)
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
}
