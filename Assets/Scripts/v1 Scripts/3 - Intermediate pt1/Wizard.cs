﻿using System;
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

    public enum Element
    {
        Red,
        Blue,
        Green
    }

    public Element currentElement = Element.Red;

    // Start is called before the first frame update
    void Start()
    {
        _iDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        Health = 1;//10;
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
            //Debug.Log("lvlup");
            this.level++;
            expCap *= 10;
            Health += 10;
            DisplayStats();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (currentElement == Element.Red)
            {
                currentElement = Element.Green;
                UtilityHelper.ChangeColor(this.gameObject, Color.green);
            }
            else if (currentElement == Element.Green)
            {
                currentElement = Element.Blue;
                UtilityHelper.ChangeColor(this.gameObject, Color.blue);
            }
            else if (currentElement == Element.Blue)
            {
                currentElement = Element.Red;
                UtilityHelper.ChangeColor(this.gameObject, Color.red);
            }
        }

        if(!isOnSpellCD)
            foreach (var spell in spells)
            {
                if (spell.lvlRequired == this.level)
                {
                    OnCast?.Invoke(currentElement.ToString() + " " + level);//spell.name);
                }
            }
        



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _iDB.AddRune(0, this);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            _iDB.RemoveRune(0, this);
        }

        if (Health <= 0 && level > 0)
        {
            //Destroy(this.gameObject);
        }
    }

    public Vector3 RetPos()
    {
        return transform.position;
    }

    public Color RetColor()
    {
        return this.GetComponent<MeshRenderer>().material.color;
    }

    public int Cast(Vector3 enemyPos, Color enemyElement)
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

                    StartCoroutine(SpellEffectAnimation(spellEffect, spell.spellCD, spellEffect.transform.position, enemyPos));
                    isOnSpellCD = true;
                    StartCoroutine(SpellCoolDownTimer(spell.spellCD));

                    //UtilityHelper.ChangeColor(spellEffect, spell.spellColor);
                    if (currentElement == Element.Red)
                    {
                        UtilityHelper.ChangeColor(spellEffect, Color.red);
                        return Mathf.CeilToInt(spell.spellDmg * UtilityHelper.GetElementMod(enemyElement, Color.red));
                    }
                    else if (currentElement == Element.Green)
                    {
                        UtilityHelper.ChangeColor(spellEffect, Color.green);
                        return Mathf.CeilToInt(spell.spellDmg * UtilityHelper.GetElementMod(enemyElement, Color.green));
                    }
                    else if (currentElement == Element.Blue)
                    {
                        UtilityHelper.ChangeColor(spellEffect, Color.blue);
                        return Mathf.CeilToInt(spell.spellDmg * UtilityHelper.GetElementMod(enemyElement, Color.blue));
                    }
                    //Debug.Log("Spell Damage: " + spell.spellDmg);
                    else
                    {
                        Debug.Log("none"); 
                        return Mathf.CeilToInt(spell.spellDmg * UtilityHelper.GetElementMod(enemyElement, spell.spellColor));

                    }
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
                OnCast?.Invoke(currentElement.ToString() + " " + level);//spell.name);
            }
        }
        isOnSpellCD = false;
    }

    IEnumerator SpellEffectAnimation(GameObject Effect, float CD, Vector3 origin, Vector3 destination)
    {
        //yield return new WaitForSeconds(CD);
        while (Effect.transform.position.y >= destination.y)
        {
            if (level <= 0) yield break;
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
        //GetComponent<MeshRenderer>().material.color = Color.yellow;
        Health -= dmgAmount;
        if (OnDamage != null)
        {
            OnDamage(Health);
        }
        //Debug.Log("Quack! The Wizard Hit himself! HP: " + Health);
    }

    public void DisplayStats()
    {
        //Debug.Log("display");
        if (OnLvlUp != null)
        {
            foreach (var spell in spells)
            {
                if (spell.lvlRequired == this.level)
                    OnLvlUp(level, currentElement.ToString() + " " + level);//spell.name);
            }
        }
        if (OnDamage != null)
        {
            OnDamage(Health);
        }
    }

    public void ResetWizard()
    {
        if (!GameManager.Instance.isGameOver)
        {
            if (level == 1)
                StopAllCoroutines();
            level--;
        }
        else
        {
            level = 1;
            exp = 0;
            expCap = 10;
            Health = 10;
            runes = null;
            GetComponent<MeshRenderer>().material.color = defaultColor;
            this.gameObject.SetActive(true);
            DisplayStats();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Wizard: OnTriggerEnter");
        if (other.tag == "Enemy")
        {
            //Debug.Log("Enemy hit Wizard");
            other.GetComponent<Bandit>().Attack(this);
            other.GetComponent<Bandit>().Die();
        }
    }
}
