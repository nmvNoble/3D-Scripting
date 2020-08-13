using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public Spell[] spells;

    public int level = 1;
    public int exp;
    public int expCap = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach(var spell in spells)
            {
                if(spell.lvlRequired == this.level)
                    this.exp += spell.Cast();
            }
        }

        if(this.exp == expCap)
        {
            this.level++;
            expCap *= 10;
        }
    }
}
