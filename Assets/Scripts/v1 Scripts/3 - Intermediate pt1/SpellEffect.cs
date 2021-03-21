using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public Spell currentSpell;
    public float spellTotalDamage;//currentWizLevel;
    //public Color spellElement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpellEffect(Spell newSpell, int wizLevel)
    {
        currentSpell = newSpell;
        spellTotalDamage = currentSpell.spellDmgMod * wizLevel;
    }

    public void SetCurrentSpell(Spell newSpell)
    {
        currentSpell = newSpell;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Debug.Log((currentSpell.spellDmg + currentWizLevel) + " * " + UtilityHelper.GetElementMod(other.GetComponent<Enemy>().RetColor(), currentSpell.spellColor));

            //other.gameObject.GetComponent<Enemy>().HitBySpell(this);
            //other.gameObject.GetComponent<Enemy>().LogStats(other.name+"-----");
            ////Debug.Log("---------, HP: " + other.gameObject.GetComponent<Enemy>().Health + ", speed: " + other.gameObject.GetComponent<Enemy>().speed + ", dmg: " + other.gameObject.GetComponent<Enemy>().damage);
            //other.gameObject.GetComponent<Enemy>().HitBySpell(currentSpell.spellDmg, currentWizLevel, currentSpell.spellColor);//.Damage(

            //Mathf.CeilToInt(
            //currentSpell.spellDmg + currentWizLevel) *
            //UtilityHelper.GetElementMod(other.GetComponent<Enemy>().RetColor(), currentSpell.spellColor));
        }
    }
}
