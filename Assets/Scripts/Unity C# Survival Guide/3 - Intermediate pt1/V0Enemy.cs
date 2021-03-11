using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class V0Enemy : MonoBehaviour
{
    public int speed, health, exp;

    public abstract void Attack();
    public virtual void Die()
    {
        Destroy(this.gameObject, Random.Range(2, 6));
    }
}


