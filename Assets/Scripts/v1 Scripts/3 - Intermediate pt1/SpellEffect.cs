using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public Spell currentSpell;
    public float spellTotalDamage;

    void Update()
    {

        if (GameManager.Instance.IsGameOver)
            Destroy(this);
    }

    public void OnEnable()
    {
        if (this != null)
            Destroy(this, 5f);
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
}
