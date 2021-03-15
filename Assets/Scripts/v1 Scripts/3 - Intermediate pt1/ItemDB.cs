using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [SerializeField]
    private Dictionary<int, Rune> _runes;
    public Dictionary<int, Rune> Runes
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

    public List<Consumable> consumables = new List<Consumable>();
    public List<Weapon> weapons = new List<Weapon>();


    [SerializeField]
    Item note1, note2;
    Rune rKnowledge, rQuickness, rDestruction, rRange;

    // Start is called before the first frame update
    void Start()
    {
        note1 = new Item("note 1", 0, "sword is Sharp boi.", 2);
        note2 = CreateItem("note 2", 1, "staff is Bonky boi.", 2);
        rKnowledge = CreateRune("Knowledge", 0, "You Gain More Experience when a spell has this rune.", 0, 2f);
        rQuickness = CreateRune("Quickness", 1, "You Cast Quicker when a spell has this rune.", 1, .5f);
        rDestruction = CreateRune("Destruction", 2, "Your spells Deal More Damage when a spell has this rune.", 2, 2f);
        rRange = CreateRune("Range", 3, "Your spells Reach Farther when a spell has this rune.", 3, 2f);
        Runes = new Dictionary<int, Rune>
        {
            { rKnowledge.id, rKnowledge },
            { rQuickness.id, rQuickness },
            { rDestruction.id, rDestruction },
            { rRange.id, rRange }
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

    private Rune CreateRune(string name, int id, string description,
            int spellStat, float runeEffect)
    {
        var rune = new Rune(name, id, description, spellStat, runeEffect);
        return rune;
    }
    private Item CreateItem(string name, int id, string desc, int type)
    {
        var item = new Item(name, id, desc, type);
        return item;
    }

    public void AddItem(int index, Wizard player)
    {
        foreach (var item in _items)
        {
            if (index == item.id)
            {
                if (item != null && player.runes[index].name == item.name)
                {
                    Debug.Log("Rune already aquired!");
                    return;
                } else
                player.items[index] = item;
                Debug.Log("Aquired " + item.name);
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

    public void AddRune(int index, Wizard player)
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
    public void RemoveRune(int index, Wizard player)
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
