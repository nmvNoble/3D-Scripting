using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public Spell currentSpell;
    public int currentWizLevel;
    //public Color spellElement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentSpell(Spell newSpell)
    {
        currentSpell = newSpell;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log((currentSpell.spellDmg + currentWizLevel) + " * " + UtilityHelper.GetElementMod(other.GetComponent<Enemy>().RetColor(), currentSpell.spellColor));
            other.GetComponent<Enemy>().Damage(
                    Mathf.CeilToInt(
                    currentSpell.spellDmg + currentWizLevel) *
                    UtilityHelper.GetElementMod(other.GetComponent<Enemy>().RetColor(), currentSpell.spellColor));
        }
    }
}
