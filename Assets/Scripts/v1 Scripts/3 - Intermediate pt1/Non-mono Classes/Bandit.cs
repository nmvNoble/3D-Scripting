using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bandit : Enemy, IDamagable
{
    private UIManager _ui;

    public float Health { get; set; }
    [SerializeField]
    private TextMesh HpText;
    private Color defaultColor;

    private GameObject target;
    private Vector3 lookAt;

    private void Start()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
        target = GameObject.Find("Wizard");
        speed = 2f;//.01f;
        damage = 1;
        //initialPos = transform.position;
    }

    private void Update()
    {
        //step += Time.deltaTime * speed;
        //// Moves the object to target position
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        HpText.text = Health.ToString();
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
    
    public void HitBySpell(SpellEffect spellEffect)
    {
        //Debug.Log("Dmg breakdown: ("+ spellEffect.currentSpell.spellDmg+" + "+ spellEffect.currentWizLevel+") * "+
          //      UtilityHelper.GetElementMod(RetColor(), spellEffect.currentSpell.spellColor));
        Damage(Mathf.CeilToInt(
            (spellEffect.currentSpell.spellDmg + spellEffect.currentWizLevel) *
                    UtilityHelper.GetElementMod(RetColor(), spellEffect.currentSpell.spellColor)));
        //Damage(other.GetComponent<SpellEffect>().currentSpell.spellDmg);

        //other.GetComponent<Bandit>().Attack(this);
        //other.GetComponent<Bandit>().Die();
    }
    public void Damage(float dmgAmount)
    {
        if(dmgAmount > 0)
        {
            //GetComponent<MeshRenderer>().material.color = Color.magenta;
            Health -= dmgAmount;
            //Debug.Log("Bandit Damage Taken: " + dmgAmount);
            //Debug.Log("Bandit HP: " + Health);
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
        //Health = GameManager.Instance.wave + (GameManager.Instance.wave/2); 
        //GetComponent<MeshRenderer>().material.color = defaultColor;
        //Debug.Log("Bandit Dying");
        this.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        Health = GameManager.Instance.wave + (GameManager.Instance.wave / 2);
        speed = 2f + (GameManager.Instance.wave * .01f);
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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name + " hit Bandit(OnTriggerEnter)");
        if (other.name == "Sphere")
        {
            HitBySpell(other.GetComponent<SpellEffect>());
        }
    }
}
