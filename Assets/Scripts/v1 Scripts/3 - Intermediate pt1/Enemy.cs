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
    private Vector3 lookAt, scale;
    private float defaultDamage, defaultSpeed;
    private bool isBeingDamaged;


    private void Start()
    {
        target = GameObject.Find("Wizard");
        speed = 2f;
        defaultSpeed = speed;
        damage = 1f;
        defaultDamage = damage;
    }

    private void Update()
    {
        lookAt = (target.transform.position - transform.position).normalized;
        transform.Translate(lookAt * Time.deltaTime * speed);
        /*speed = .01f;
        step += Time.deltaTime * speed;
        // Moves the object to target position
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);*/
    }

    public virtual void OnEnable()
    {
        UIManager.Instance.UpdateEnemyCount();
        Health = GameManager.Instance.wave + (GameManager.Instance.wave / 2);
        speed = defaultSpeed + (GameManager.Instance.wave * .05f);
        damage = (GameManager.Instance.wave / 2);
        HpText.text = Health.ToString();
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
            if (other.name == "Wizard")
            {
                Attack(other.GetComponent<IDamagable>());
                Die();
            }
        }
    }
}


