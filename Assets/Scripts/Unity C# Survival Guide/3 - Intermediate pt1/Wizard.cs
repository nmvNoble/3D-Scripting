using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour, IDamagable
{
    private ItemDB _iDB;
    public Spell[] spells;
    public Item[] runes = new Item[9];

    public int level = 1;
    public int exp;
    public int expCap = 10;
    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _iDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        Health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cast();
        }

        if (this.exp == expCap)
        {
            this.level++;
            expCap *= 10;
            Health += 10;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _iDB.AddRune(0, this);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _iDB.RemoveRune(0, this);
        }

        if (Health <= 0)
        {
            Debug.Log("The Wizard has fallen!");
            Destroy(this.gameObject);
        }
    }

    public void Cast()
    {
        foreach (var spell in spells)
        {
            if (spell.lvlRequired == this.level)
                this.exp += spell.Cast();
        }
    }

    public void Damage(int dmgAmount)
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        Health -= dmgAmount;
        Debug.Log("Quack! The Wizard Hit himself! HP: " + Health);
    }
}
