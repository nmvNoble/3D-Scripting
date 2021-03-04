using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy, IDamagable
{
    private UIManager _ui;

    public int Health { get; set; }
    [SerializeField]
    private Color defaultColor;

    private GameObject target;
    private Vector3 lookAt;
    public float step=0;

    private void Start()
    {
        UtilityHelper.ChangeColor(this.gameObject, Color.red);
        defaultColor = GetComponent<MeshRenderer>().material.color;
        target = GameObject.Find("Wizard");
        speed = .01f;
        damage = 1;
        //initialPos = transform.position;
        //_ui = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    private void Update()
    {
        //step += Time.deltaTime * speed;
        //// Moves the object to target position
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

        lookAt = (target.transform.position - transform.position).normalized;
        transform.Translate(lookAt * Time.deltaTime * speed);
        //if (Vector3.Distance(transform.position, target.transform.position) < .75f)
            //Die();
    }

    public override void Attack(IDamagable taget)
    {
        taget.Damage(damage);
    }

    public Vector3 RetPos()
    {
        return transform.position;
    }

    public Color RetColor()
    {
        return this.GetComponent<MeshRenderer>().material.color;
    }
    

    public void Damage(int dmgAmount)
    {
        if(dmgAmount > 0)
        {
            //GetComponent<MeshRenderer>().material.color = Color.magenta;
            Health -= dmgAmount;
            //Debug.Log("Bandit Damage Taken: " + dmgAmount);
            Debug.Log("Bandit HP: " + Health);
            if (Health <= 0)
            {
                //Destroy(this.gameObject);
                this.CancelInvoke();
                Die();
            }
        }
    }

    public override void Die()
    {
        Health = 10; 
        step = 0;
        //GetComponent<MeshRenderer>().material.color = defaultColor;
        //Debug.Log("Bandit Dying");
        this.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        Health = 10;
        SpawnManager.enemyCount++;
        //_ui.UpdateEnemyCount();
        UIManager.Instance.UpdateEnemyCount();

        //Invoke("Die", Random.Range(2, 6));
        //defaultColor = GetComponent<MeshRenderer>().material.color;
        //initialPos = transform.position;
    }

    public void OnDisable()
    {
        SpawnManager.enemyCount--;
        //_ui.UpdateEnemyCount();
        UIManager.Instance.UpdateEnemyCount();
    }
}
