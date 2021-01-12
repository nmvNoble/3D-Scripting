using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface V0IDamagable
{
    int Health { get; set; }

    void Damage(int dmgAmount);
}
