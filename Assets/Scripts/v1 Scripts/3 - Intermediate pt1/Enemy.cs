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
        /*speed = 2f;
        defaultSpeed = speed;
        damage = 1f;
        defaultDamage = damage;*/
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
        SetEnemyType(Random.Range(1, 4));
        //Debug.Log("=========type: ^" + ", HP: " + Health + ", speed: " + speed + ", dmg: " + damage);
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
                Health = GameManager.Instance.wave;
                this.speed = 3f + (GameManager.Instance.wave * .1f);
                this.damage = 1 + (GameManager.Instance.wave / 4);
                break;
            case 2:
                this.transform.localScale = new Vector3(1, 2, 1);
                Health = GameManager.Instance.wave + (GameManager.Instance.wave / 2);
                this.speed = 2.5f + (GameManager.Instance.wave * .05f);
                this.damage = 1 + (GameManager.Instance.wave / 2);
                break;
            case 3:
                this.transform.localScale = new Vector3(2, 2, 2);
                Health = GameManager.Instance.wave * 2;
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


