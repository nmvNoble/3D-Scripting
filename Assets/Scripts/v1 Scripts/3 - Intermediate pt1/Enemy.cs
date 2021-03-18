using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float Health { get; set; }

    public float damage, speed;

    [SerializeField]
    private TextMesh HpText;
    private GameObject target;
    private Vector3 lookAt;


    private void Start()
    {
        target = GameObject.Find("Wizard");
        speed = 2f;
        damage = 1f;
    }

    private void Update()
    {
        HpText.text = Health.ToString();
        lookAt = (target.transform.position - transform.position).normalized;
        transform.Translate(lookAt * Time.deltaTime * speed);
        /*speed = .01f;
        step += Time.deltaTime * speed;
        // Moves the object to target position
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);*/
    }

    public virtual void OnEnable()
    {
        Health = GameManager.Instance.wave + (GameManager.Instance.wave / 2);
        speed += (GameManager.Instance.wave * .01f);
        damage = 1;
        UIManager.Instance.UpdateEnemyCount();
    }

    public Vector3 RetPos()
    {
        return transform.position;
    }

    public Color RetColor()
    {
        return this.GetComponent<MeshRenderer>().material.color;
    }

    public virtual void Attack(IDamagable taget)
    {
        taget.Damage(damage);
    }

    public void Damage(float dmgAmount)
    {
        if (dmgAmount > 0)
        {
            Health -= dmgAmount;
            if (Health <= 0)
            {
                this.CancelInvoke();
                Die();
            }
        }
    }

    public virtual void Die()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.name == "Sphere")
        {
            HitBySpell(other.GetComponent<SpellEffect>());
        }
        if (other.name == "Wizard")
        {
            Attack(other.GetComponent<IDamagable>());
            Die();
        }
    }

    public void HitBySpell(SpellEffect spellEffect)
    {
        Damage(Mathf.CeilToInt(
            (spellEffect.currentSpell.spellDmg + spellEffect.currentWizLevel) *
                    UtilityHelper.GetElementMod(RetColor(), spellEffect.currentSpell.spellColor)));
    }
}


