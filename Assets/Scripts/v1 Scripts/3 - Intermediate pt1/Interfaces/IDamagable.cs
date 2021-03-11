using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    float Health { get; set; }

    Vector3 RetPos();

    Color RetColor();

    void Damage(float dmgAmount);
}
