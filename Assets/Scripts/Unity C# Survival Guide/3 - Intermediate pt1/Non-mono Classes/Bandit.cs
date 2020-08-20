using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy, IDamagable
{
    private UIManager _ui;

    public int Health { get; set; }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Damage(int dmgAmount)
    {
        GetComponent<MeshRenderer>().material.color = Color.magenta;
        Health -= dmgAmount;
        if (Health <= 0)
            Destroy(this.gameObject);
        Debug.Log("Bandit - Magenta! HP: "+Health);
    }

    public override void Die()
    {
        base.Die();
    }

    public void OnEnable()
    {
        Health = 10;
        SpawnManager.enemyCount++;
        _ui = GameObject.Find("UI Manager").GetComponent<UIManager>();
        _ui.UpdateEnemyCount();
        Die();
    }

    public void OnDisable()
    {
        SpawnManager.enemyCount--;
        _ui.UpdateEnemyCount();
    }
}
