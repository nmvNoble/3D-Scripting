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
    public static Action<string> OnCast;

    private Color defaultColor;
    private GameObject spellEffect;
    private bool isOnSpellCD = false;

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
        if (!isOnSpellCD)
        {
            foreach (var spell in spells)
            {
                //Debug.Log("lvl " + level + ", lvlreq: "+ spell.lvlRequired);
                if (spell.lvlRequired == this.level)
                {
                    this.exp += spell.Cast(enemyPos);
                    //Debug.Log("Wizard has " + exp + " Total Exp");

                    spellEffect = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    spellEffect.transform.position =
                            new Vector3(enemyPos.x, enemyPos.y + spell.spellRadius, enemyPos.z);
                    spellEffect.transform.localScale =
                            new Vector3(spell.spellRadius, spell.spellRadius, spell.spellRadius);
                    UtilityHelper.ChangeColor(spellEffect, spell.spellColor);

                    StartCoroutine(SpellEffectAnimation(spellEffect, spell.spellCD, spellEffect.transform.position, enemyPos));
                    isOnSpellCD = true;
                    StartCoroutine(SpellCoolDownTimer(spell.spellCD));
                    return spell.spellDmg;
                }
            }
            Debug.Log("The Wizard does not have a Cast-able Spell!!!");
            OnCast?.Invoke("Nothing!!!");
            return 0;
        } else 
        Debug.Log("The Wizard is on Cool Down! They cannot Cast yet.");
        OnCast?.Invoke("On Cool Down!");
        return 0;
    }

    IEnumerator SpellCoolDownTimer(float spellCD)
    {
        OnCast?.Invoke("On Cool Down!");
        yield return new WaitForSeconds(spellCD);
        foreach (var spell in spells)
        {
            if (spell.lvlRequired == this.level)
            {
                OnCast?.Invoke(spell.name);
            }
        }
        isOnSpellCD = false;
    }

    IEnumerator SpellEffectAnimation(GameObject Effect, float CD, Vector3 origin, Vector3 destination)
    {
        //yield return new WaitForSeconds(CD);
        while (Effect.transform.position.y >= destination.y)
        {
            Vector3 toFace = destination - origin;
            Effect.transform.Translate(new Vector3(0, -0.05f, 0));//toFace);
            yield return new WaitForEndOfFrame();
            //Debug.Log("mid, Effect.transform.position.y" + Effect.transform.position.y + " <= destination.y" + destination.y);
            if (Effect.transform.position.y <= destination.y)
                Destroy(Effect);
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
