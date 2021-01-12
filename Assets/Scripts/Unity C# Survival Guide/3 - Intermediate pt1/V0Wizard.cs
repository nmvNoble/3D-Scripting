using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0Wizard : MonoBehaviour, V0IDamagable
{
    private V0ItemDB _iDB;
    public V0Spell[] spells;
    public V0Item[] runes = new V0Item[9];

    public int level = 1;
    public int exp;
    public int expCap = 10;
    public int Health { get; set; }
    public static Action<int> OnDamage;
    public static Action<int, string> OnLvlUp;

    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        _iDB = GameObject.Find("ItemDB").GetComponent<V0ItemDB>();
        Health = 10;
        defaultColor = GetComponent<MeshRenderer>().material.color;
        DisplayStats();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Cast();
        //}

        if (this.exp == expCap)
        {
            this.level++;
            expCap *= 10;
            Health += 10;
            DisplayStats();
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
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
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
        if (OnDamage != null)
        {
            OnDamage(Health);
        }
        Debug.Log("Quack! The Wizard Hit himself! HP: " + Health);
    }

    public void DisplayStats()
    {
        Debug.Log("Displaying Wizard Stats...");
        if (OnLvlUp != null)
        {
            foreach (var spell in spells)
            {
                if (spell.lvlRequired == this.level)
                    OnLvlUp(level, spell.name);
            }
        }
        if (OnDamage != null)
        {
            OnDamage(Health);
        }
    }

    public void ResetWizard()
    {
        level = 1;
        exp = 0;
        expCap = 10;
        Health = 10;
        runes = null;
        GetComponent<MeshRenderer>().material.color = defaultColor;
        this.gameObject.SetActive(true);
        if (OnDamage != null)
        {
            OnDamage(Health);
        }
    }
}
