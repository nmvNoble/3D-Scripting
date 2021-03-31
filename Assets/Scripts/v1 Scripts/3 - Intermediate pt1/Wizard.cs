using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour, IDamagable
{
    public static Action OnDeath;
    public static Action<float> OnDamage;
    public static Action<int, string> OnLvlUp;
    public static Action<string> OnCast;

    //private ItemDB iDB;
    //public List<Rune> runes;
    //public Item[] items = new Item[3];

    public Rune[] runes = new Rune[4];
    public Spell currSpell;
    public Spell[] spells;

    public float Health { get; set; }
    public enum Element
    {
        Red,
        Green,
        Blue
    }
    public int Exp { get { return exp; } }
    public int ExpCap { get { return expCap; } }
    public int Level { get { return level; } }

    [SerializeField]
    private TextMesh wizHpText;
    private GameObject spellObject;
    private Color defaultColor;
    private Element currentElement = Element.Red;
    private Vector3 startingPos;
    private bool isOnSpellCD = false;
    private int exp, expCap = 10, level = 1;
    private float speed = 3, horizontalInput, verticalInput;


    void Start()
    {
        Enemy.OnEnemyDeath += GainExp;

        Health = 10;
        wizHpText.text = Health.ToString();
        startingPos = RetPos();
        UtilityHelper.ChangeColor(this.gameObject, Color.red);
        defaultColor = GetComponent<MeshRenderer>().material.color;
        foreach (Spell spell in spells)
            spell.SetDefaultSpellStats();
        SetCurrentSpell(1);
        DisplayStats();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
            this.transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed * Time.deltaTime);
        if (Input.GetAxis("Vertical") != 0)
            this.transform.Translate(new Vector3(0, 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime);

        if (transform.position.z < -6.5f)
            transform.position = new Vector3(RetPos().x, RetPos().y, -6.5f);
        if (transform.position.z > 5f)
            transform.position = new Vector3(RetPos().x, RetPos().y, 5f);
        if (transform.position.x < -5f)
            transform.position = new Vector3(-5f, RetPos().y, RetPos().z);
        if (transform.position.x > 5f)
            transform.position = new Vector3(5f, RetPos().y, RetPos().z);

        if (Input.GetKeyDown(KeyCode.Mouse1))
            ChangeElement();

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


        /*if (Input.GetKeyDown(KeyCode.Alpha6))
            ApplyRune(0, currSpell.lvlRequired);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            ApplyRune(1, currSpell.lvlRequired);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            ApplyRune(2, currSpell.lvlRequired);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            ApplyRune(3, currSpell.lvlRequired);

        if (Input.GetKeyDown(KeyCode.Alpha0))
            ResetRunes();*/
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

    private void ChangeElement()
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

    private void SetCurrentSpell(int spellKey)
    {
        foreach (var spell in spells)
        {
            if (spell.lvlRequired == spellKey && spell.lvlRequired <= this.level)
            {
                currSpell = spell;
                if (!isOnSpellCD)
                    DisplaySpell();
                if (currSpell.runeSlot != null && currSpell.runeSlot.spellStat > 0 )
                    currSpell.ApplyRune();
            }
        }
    }

    public void Cast(Vector3 enemyPos, Color enemyElement)
    {
        GameManager.Instance.CheckExistingSpellEffects();

        if (!isOnSpellCD)
        {
            spellObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            spellObject.AddComponent<SpellEffect>();
            spellObject.AddComponent<SphereCollider>();
            spellObject.GetComponent<SphereCollider>().isTrigger = true;
            spellObject.GetComponent<SpellEffect>().SetSpellEffect(currSpell, level);
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
                Debug.Log("No Element Found");

            StartCoroutine(SpellEffectAnimation(spellObject, currSpell.spellCD, spellObject.transform.position, enemyPos));
            isOnSpellCD = true;
            StartCoroutine(SpellCoolDownTimer(currSpell.spellCD));
        }
        else
        {
            OnCast?.Invoke("On Cool Down!");
        }
        
    }

    public void GainExp(int expGained)
    {
        exp += expGained;
        UIManager.Instance.UpdateWizardExp(exp, expCap);
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
        while (Effect != null && Effect.transform.position.y >= destination.y)
        {
            if (level <= 0) yield break;
            //Vector3 toFace = destination - origin;
            Effect.transform.Translate(new Vector3(0, -0.05f, 0));//toFace);
            yield return new WaitForEndOfFrame();
            if (Effect != null && Effect.transform.position.y <= destination.y)
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
        Health -= dmgAmount;
        wizHpText.text = Health.ToString();
        OnDamage?.Invoke(Health);
    }

    public void DisplayStats()
    {
        if (OnLvlUp != null)
        {
            foreach (var spell in spells)
            {
                if (spell.lvlRequired <= this.level)
                    OnLvlUp(level, currSpell.name + " " + currentElement.ToString());
            }
        }
        OnDamage?.Invoke(Health);
        wizHpText.text = Health.ToString();
        UIManager.Instance.UpdateWizardExp(exp, expCap);
    }

    public void LevelUp()
    {
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
        if (!GameManager.Instance.IsGameOver)
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
            if (level < 3)
                exp = expCap / 10;
            else if (level >= 3)
                exp /= 2;
            if (currSpell.lvlRequired > level && level >= 1)
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
            wizHpText.text = Health.ToString();
            //GetComponent<MeshRenderer>().material.color = defaultColor;
            this.gameObject.transform.position = startingPos;
            SetCurrentSpell(1);
            ResetRunes();
        }
        isOnSpellCD = false;
        this.gameObject.SetActive(true);
        DisplayStats();
    }
}
