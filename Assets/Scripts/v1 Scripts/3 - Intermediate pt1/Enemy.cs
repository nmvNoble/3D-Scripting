using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int damage, exp;
    public float speed;

    public abstract void Attack(IDamagable target);
    public virtual void Die()
    {
        Destroy(this.gameObject);//, Random.Range(2, 6));
    }
}


