using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public static Action<int> OnEnemyDeath;

    public float Health { get; set; }
    public bool IsBugged { get { return isBugged; } }

    [SerializeField]
    private TextMesh HpText;

    private bool isBeingDamaged, isBugged = false;
    private int exp;
    private float damage, speed;
    private GameObject target;
    private SpellEffect spellHitBy;
    private Vector3 lookAt;


    private void Start()
    {
        Health = 1;
        target = GameObject.Find("Wizard");
    }

    private void Update()
    {
        lookAt = (target.transform.position - transform.position).normalized;
        transform.Translate(lookAt * Time.deltaTime * speed);
        if (isBeingDamaged && spellHitBy != null )
        {
            Damage(Mathf.CeilToInt(spellHitBy.spellTotalDamage) * 
                    UtilityHelper.GetElementMod(RetColor(), spellHitBy.currentSpell.spellColor));
        }
        if(Vector3.Distance(this.transform.position, target.transform.position) <= .75f)
        {
            Debug.Log("OnTriggerEnter not Working, Enemy Bugged");
            isBugged = true;
            Die();
        }
    }

    public virtual void OnEnable()
    {
        SetEnemyType();
        if(isBugged)
            Die();
        //LogStats(this.gameObject.name + "=====onEnable====type: ^");
    }

    public Vector3 RetPos()
    {
        return transform.position;
    }

    public Color RetColor()
    {
        return this.GetComponent<MeshRenderer>().material.color;
    }

    public void SetEnemyType()
    {
        switch (SpawnManager.Instance.DetermineEnemyType())
        {
            case 1:
                this.transform.position = transform.position + new Vector3(0, .75f, 0);
                this.transform.localScale = new Vector3(.75f, .75f, .75f);
                this.Health = GameManager.Instance.Wave;
                this.speed = 3f + (GameManager.Instance.Wave * .1f);
                this.damage = 1 + (GameManager.Instance.Wave / 4);
                this.exp = 1;
                SpawnManager.Instance.UpdateEnemyTypeCount(1);
                break;
            case 2:
                this.transform.localScale = new Vector3(1, 2, 1);
                this.Health = GameManager.Instance.Wave + (GameManager.Instance.Wave / 2);
                this.speed = 2.5f + (GameManager.Instance.Wave * .05f);
                this.damage = 1 + (GameManager.Instance.Wave / 2);
                this.exp = 2;
                SpawnManager.Instance.UpdateEnemyTypeCount(2);
                break;
            case 3:
                this.transform.localScale = new Vector3(2, 2, 2);
                this.Health = GameManager.Instance.Wave * 2;
                this.speed = 2f + (GameManager.Instance.Wave * .01f);
                this.damage = 1 + GameManager.Instance.Wave;
                this.exp = 3;
                SpawnManager.Instance.UpdateEnemyTypeCount(3);
                break;
        }
        //Debug.Log("type: " + type);// + ", HP: " + Health + ", speed: " + speed + ", dmg: " + damage);
        HpText.text = Health.ToString(); 
    }

    public virtual void Attack(IDamagable target)
    {
        if(damage > 0)
        {
            target.Damage(Mathf.CeilToInt(damage) *
                        UtilityHelper.GetElementMod(target.RetColor(), RetColor()));
        }
    }

    public void HitBySpell(SpellEffect spellEffect)
    {
        isBeingDamaged = true;
        spellHitBy = spellEffect;
    }

    public void Damage(float dmgAmount)
    {
        if (dmgAmount > 0 && this.damage > 0)
        {
            Health -= dmgAmount;
            HpText.text = Health.ToString();
            if (Health <= 0)
            {
                this.CancelInvoke();
                Die();
                if (OnEnemyDeath != null && isBeingDamaged)
                {
                    OnEnemyDeath(exp);
                }
            }
            isBeingDamaged = false;
        }
    }

    public virtual void Die()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!isBeingDamaged)
        {
            if (other.name == "Sphere")
            {
                HitBySpell(other.GetComponent<SpellEffect>());}
            if (other.name == "Wizard")
            {
                Attack(other.GetComponent<IDamagable>());
                Die();
            }
        }
    }
    
    public void LogStats(string intro = null)
    {
        Debug.Log(intro + ", HP: " + Health + ", speed: " + speed + ", dmg: " + damage);
    }
}


