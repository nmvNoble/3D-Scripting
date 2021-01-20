using System;
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
    public static Action<int> OnDamage;
    public static Action<int, string> OnLvlUp;

    private Color defaultColor;
    private GameObject spellEffect;

    // Start is called before the first frame update
    void Start()
    {
        _iDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
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

    public Vector3 RetPos()
    {
        return transform.position;
    }

    public int Cast(Vector3 enemyPos)
    {
        foreach (var spell in spells)
        {
            Debug.Log("lvl " + level + ", lvlreq: "+ spell.lvlRequired);
            if (spell.lvlRequired == this.level)
            {
                this.exp += spell.Cast(enemyPos);
                Debug.Log("Wizard has " + exp + " Total Exp");

                spellEffect = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                spellEffect.transform.position =
                        new Vector3(enemyPos.x, enemyPos.y + spell.spellRadius, enemyPos.z);
                spellEffect.transform.localScale =
                        new Vector3(spell.spellRadius, spell.spellRadius, spell.spellRadius);
                UtilityHelper.ChangeColor(spellEffect, spell.spellColor);
                StartCoroutine(SpellEffectAnimation(spellEffect, spell.spellCD));//, spellEffect.transform.position, enemyPos));

                return spell.spellDmg;
            }
        }
        Debug.Log("The Wizard does not have a Cast-able Spell!!!");
        return 0;
    }

    IEnumerator SpellEffectAnimation(GameObject Effect, float CD)//, Vector3 origin, Vector3 destination)
    {
            yield return new WaitForSeconds(CD);
            /*while(origin != destination)
            {
                Vector3 toFace = destination - origin;
                Effect.transform.Translate(toFace);
                yield return new WaitForEndOfFrame();
            }*/
            Destroy(Effect);
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
