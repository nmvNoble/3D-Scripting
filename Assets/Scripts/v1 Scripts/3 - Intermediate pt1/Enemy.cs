using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float Health { get; set; }
    [SerializeField]
    private float damage, speed;

    [SerializeField]
    private TextMesh HpText;
    private GameObject target;
    private Vector3 lookAt, scale;
    private float defaultDamage, defaultSpeed;
    private bool isBeingDamaged;
    [SerializeField]
    private SpellEffect spellHitBy;


    private void Start()
    {
        Health = 1;
        target = GameObject.Find("Wizard");
        /*speed = 2f;
        defaultSpeed = speed;
        damage = 1f;
        defaultDamage = damage;*/
    }

    private void Update()
    {
        lookAt = (target.transform.position - transform.position).normalized;
        transform.Translate(lookAt * Time.deltaTime * speed);
        if (isBeingDamaged)
        {
            /*LogStats("update~~~~~~~~~~~");
            Debug.Log(spellHitBy.currentSpell.spellDmg + spellHitBy.currentWizLevel + " * " +
                    UtilityHelper.GetElementMod(RetColor(), spellHitBy.currentSpell.spellColor));*/
            Damage(Mathf.CeilToInt(spellHitBy.currentSpell.spellDmg + spellHitBy.currentWizLevel) *
                    UtilityHelper.GetElementMod(RetColor(), spellHitBy.currentSpell.spellColor));
        }
        /*speed = .01f;
        step += Time.deltaTime * speed;
        // Moves the object to target position
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);*/
    }

    public virtual void OnEnable()
    {
        UIManager.Instance.UpdateEnemyCount();
        SetEnemyType(Random.Range(1, 4));
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

    public void SetEnemyType(int type)
    {
        switch (type)
        {
            case 1:
                this.transform.position = transform.position + new Vector3(0, .75f, 0);
                this.transform.localScale = new Vector3(.75f, .75f, .75f);
                this.Health = GameManager.Instance.wave;
                this.speed = 3f + (GameManager.Instance.wave * .1f);
                this.damage = 1 + (GameManager.Instance.wave / 4);
                break;
            case 2:
                this.transform.localScale = new Vector3(1, 2, 1);
                this.Health = GameManager.Instance.wave + (GameManager.Instance.wave / 2);
                this.speed = 2.5f + (GameManager.Instance.wave * .05f);
                this.damage = 1 + (GameManager.Instance.wave / 2);
                break;
            case 3:
                this.transform.localScale = new Vector3(2, 2, 2);
                this.Health = GameManager.Instance.wave * 2;
                this.speed = 2f + (GameManager.Instance.wave * .01f);
                this.damage = 1 + GameManager.Instance.wave;
                break;
        }
        //Debug.Log("type: " + type);// + ", HP: " + Health + ", speed: " + speed + ", dmg: " + damage);
        //Debug.Log("HP: " + Health.ToString());
        HpText.text = Health.ToString(); 
    }

    public virtual void Attack(IDamagable taget)
    {
        taget.Damage(damage);
    }

    public void HitBySpell(SpellEffect spellEffect)//float spellDmg, int wizLvl, Color spellElement)
    {
        isBeingDamaged = true;
        spellHitBy = spellEffect;
        //LogStats(isBeingDamaged+"|"+spellEffect.name+" ==hitbyspell== ");
        //Damage(Mathf.CeilToInt(spellDmg + wizLvl) *
        //        UtilityHelper.GetElementMod(RetColor(), spellElement));
    }

    public void Damage(float dmgAmount)
    {
        //LogStats(this.gameObject.name + "=====damage====type: ^");
        if (dmgAmount > 0 && this.damage > 0)
        {
            //Debug.Log("HP before: " + Health + " - " + dmgAmount + "(dmg)");
            Health -= dmgAmount;
            //Debug.Log("HP after: " + Health);
            isBeingDamaged = false;
            HpText.text = Health.ToString();
            if (Health <= 0)
            {
                this.CancelInvoke();
                Die();
            }
        }
        //isBeingDamaged = false;
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
                //isBeingDamaged = true;
                HitBySpell(other.GetComponent<SpellEffect>());
                //HitBySpell(other.GetComponent<SpellEffect>().currentSpell.spellDmg,
                //    other.GetComponent<SpellEffect>().currentWizLevel, other.GetComponent<SpellEffect>().currentSpell.spellColor);
            }
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


