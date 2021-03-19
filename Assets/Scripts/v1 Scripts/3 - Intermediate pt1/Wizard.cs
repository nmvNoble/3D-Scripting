using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour, IDamagable
{
    private ItemDB _iDB;
    public Spell[] spells;
    public Spell currSpell;
    public Rune[] runes = new Rune[4];
    //public List<Rune> runes;
    public Item[] items = new Item[3];

    public int level = 1;
    public int exp;
    public int expCap = 10;
    public float Health { get; set; }
    public static Action OnDeath;
    public static Action<float> OnDamage;
    public static Action<int, string> OnLvlUp;
    public static Action<string> OnCast;

    private Color defaultColor;
    private GameObject spellObject;
    private bool isOnSpellCD = false;
    private Vector3 startingPos;

    public enum Element
    {
        Red, 
        Green,
        Blue
    }

    public Element currentElement = Element.Red;

    // Start is called before the first frame update
    void Start()
    {
        _iDB = GameObject.Find("ItemDB").GetComponent<ItemDB>();
        Health = 10;
        UtilityHelper.ChangeColor(this.gameObject, Color.red);
        defaultColor = GetComponent<MeshRenderer>().material.color;
        DisplayStats();
        foreach(Spell spell in spells)
            spell.SetDefaultSpellStats();
        SetCurrentSpell(1);
        startingPos = RetPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -6.5f)
            transform.position = new Vector3(RetPos().x, RetPos().y, -6.5f);
        if (transform.position.z > 5f)
            transform.position = new Vector3(RetPos().x, RetPos().y, 5f);
        if (transform.position.x < -5f)
            transform.position = new Vector3(-5f, RetPos().y, RetPos().z);
        if (transform.position.x > 5f)
            transform.position = new Vector3(5f, RetPos().y, RetPos().z);

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
            if (!isOnSpellCD)
                DisplaySpell();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currSpell.lvlRequired == 1 && level >= 2)
                SetCurrentSpell(2);
            else if (currSpell.lvlRequired == 2 && level >= 3)
                SetCurrentSpell(3);
            else
                SetCurrentSpell(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetCurrentSpell(1);
        if (Input.GetKeyDown(KeyCode.Alpha2) && level >= 2)
            SetCurrentSpell(2);
        if (Input.GetKeyDown(KeyCode.Alpha3) && level >= 3)
            SetCurrentSpell(3);


        if (Input.GetKeyDown(KeyCode.Alpha6))
            ApplyRune(0, currSpell.lvlRequired);

        if (Input.GetKeyDown(KeyCode.Alpha7))
            ApplyRune(1, currSpell.lvlRequired);

        if (Input.GetKeyDown(KeyCode.Alpha8))
            ApplyRune(2, currSpell.lvlRequired);

        if (Input.GetKeyDown(KeyCode.Alpha9))
            ApplyRune(3, currSpell.lvlRequired);


        if (Input.GetKeyDown(KeyCode.Alpha0))
            ResetRunes();
    }

    public Vector3 RetPos()
    {
        return transform.position;
    }

    public Color RetColor()
    {
        return this.GetComponent<MeshRenderer>().material.color;
    }

    public void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
    }

    private void SetCurrentSpell(int spellKey)
    {
        foreach (var spell in spells)
        {
            //Debug.Log("lvl " + level + ", lvlreq: "+ spell.lvlRequired);
            if (spell.lvlRequired == spellKey && spell.lvlRequired <= this.level)
            {
                currSpell = spell;
                //Debug.Log("current spell: " + currSpell.name);
                if (!isOnSpellCD)
                    DisplaySpell();
                if (currSpell.runeSlot.spellStat > 0 )
                    currSpell.ApplyRune();
            }
        }
    }

    public void Cast(Vector3 enemyPos, Color enemyElement)
    {
        if (!isOnSpellCD)
        {
            //Debug.Log("============================================================Casting: " + currSpell.name);
            this.exp += currSpell.Cast(enemyPos);
            UIManager.Instance.UpdatePlayerExp(exp, expCap);
            //Debug.Log("Wizard has " + exp + " Total Exp");

            spellObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spellObject.AddComponent<SpellEffect>();
            spellObject.AddComponent<SphereCollider>();
            spellObject.GetComponent<SphereCollider>().isTrigger = true;
            spellObject.GetComponent<SpellEffect>().currentWizLevel = level;
            spellObject.GetComponent<SpellEffect>().SetCurrentSpell(currSpell);
            spellObject.transform.position =
                    new Vector3(enemyPos.x, enemyPos.y + currSpell.spellDiameter, enemyPos.z);
            spellObject.transform.localScale =
                    new Vector3(currSpell.spellDiameter, currSpell.spellDiameter, currSpell.spellDiameter);

            if (currentElement == Element.Red)
            {
                UtilityHelper.ChangeColor(spellObject, Color.red);
                spellObject.GetComponent<SpellEffect>().currentSpell.spellColor = Color.red;
            }
            else if (currentElement == Element.Green)
            {
                UtilityHelper.ChangeColor(spellObject, Color.green);
                spellObject.GetComponent<SpellEffect>().currentSpell.spellColor = Color.green;
            }
            else if (currentElement == Element.Blue)
            {
                UtilityHelper.ChangeColor(spellObject, Color.blue);
                spellObject.GetComponent<SpellEffect>().currentSpell.spellColor = Color.blue;
            }
            else
                Debug.Log("none");

            StartCoroutine(SpellEffectAnimation(spellObject, currSpell.spellCD, spellObject.transform.position, enemyPos));
            isOnSpellCD = true;
            StartCoroutine(SpellCoolDownTimer(currSpell.spellCD));
            //Debug.Log("Spell Rune: " + currSpell.runeSlot.name);

            //UtilityHelper.ChangeColor(spellObject, spell.spellColor);
        }
        else
        {
            //Debug.Log("The Wizard is on Cool Down! They cannot Cast yet.");
            OnCast?.Invoke("On Cool Down!");
            //return 0;
        }
        
    }

    private void DisplaySpell()
    {
        if (currSpell.runeSlot != null && currSpell.runeSlot.itemType == Item.ItemType.Rune)
            OnCast?.Invoke(currSpell.name + " " + currentElement.ToString() + "\n" +
                    currSpell.runeSlot.name + " " + currSpell.runeSlot.itemType.ToString());
        else
            OnCast?.Invoke(currSpell.name + " " + currentElement.ToString());
    }

    IEnumerator SpellCoolDownTimer(float spellCD)
    {
        OnCast?.Invoke("On Cool Down!");
        yield return new WaitForSeconds(spellCD);
        isOnSpellCD = false;
        DisplaySpell();
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

    public void ApplyRune(int runeID, int spellType)
    {
        spells[spellType].runeSlot = runes[runeID];
        if (currSpell.lvlRequired == spells[spellType].lvlRequired)
        {
            currSpell.runeSlot = runes[runeID];
            currSpell.ApplyRune();
        }
        DisplaySpell();
    }

    public void ResetRunes()
    {
        foreach (Spell spell in spells)
            spell.RemoveRune();
        for (int i = 0; i <= (runes.Length - 1); i++)
        {
            runes[i] = null;
        }
        currSpell.RemoveRune();
        DisplaySpell();
    }

    public void Damage(float dmgAmount)
    {
        //GetComponent<MeshRenderer>().material.color = Color.yellow;
        Health -= dmgAmount;
        //Debug.Log("Quack! The Wizard Hit himself2! HP: " + Health);
        if (OnDamage != null)
        {
            OnDamage(Health);
        }
    }

    public void DisplayStats()
    {
        //Debug.Log("display");
        if (OnLvlUp != null)
        {
            foreach (var spell in spells)
            {
                if (spell.lvlRequired == this.level)
                    OnLvlUp(level, currSpell.name + " " + currentElement.ToString());
            }
        }
        if (OnDamage != null)
        {
            OnDamage(Health);
        }
        UIManager.Instance.UpdatePlayerExp(exp, expCap);
    }

    public void LevelUp()
    {
        //Debug.Log("lvlup");
        this.level++;
        if (level == 2)
            expCap = 50;
        else if (level == 3)
            expCap = 100;
        else if (level > 3)
            expCap += 100;
        Health += 10;
        DisplayStats();
        if (level <= 3)
            SetCurrentSpell(level);
    }

    public void ResetWizard()
    {
        if (!GameManager.Instance.isGameOver)
        {
            level--;
            if (level == 1)
                StopAllCoroutines();
            Health = level * 10;
            if (level == 1)
                expCap = 10;
            else if (level == 2)
                expCap = 50;
            else if (level == 3)
                expCap = 100;
            else if (level > 3)
                expCap -= 100;
            exp = expCap / 10;
            if (currSpell.lvlRequired > level && level > 1)
            {
                currSpell = spells[level - 1];
                StopCoroutine(SpellCoolDownTimer(0));
            }
        }
        else
        {
            level = 1;
            exp = 0;
            expCap = 10;
            Health = 10;
            GetComponent<MeshRenderer>().material.color = defaultColor;
            this.gameObject.transform.position = startingPos;
            ResetRunes();
        }
        isOnSpellCD = false;
        this.gameObject.SetActive(true);
        DisplayStats();
    }
}
