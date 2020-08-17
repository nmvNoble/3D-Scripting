using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private ItemDB _iDB;
    public Spell[] spells;
    public Item[] runes = new Item[9];

    public int level = 1;
    public int exp;
    public int expCap = 10;

    // Start is called before the first frame update
    void Start()
    {
        _iDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var spell in spells)
            {
                if (spell.lvlRequired == this.level)
                    this.exp += spell.Cast();
            }
        }

        if (this.exp == expCap)
        {
            this.level++;
            expCap *= 10;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _iDB.AddRune(0, this);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _iDB.RemoveRune(0, this);
        }
    }
}
