using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0Bandit : V0Enemy, V0IDamagable
{
    //private V0UIManager _ui;

    public int Health { get; set; }
    private Color defaultColor;

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Damage(int dmgAmount)
    {
        GetComponent<MeshRenderer>().material.color = Color.magenta;
        Health -= dmgAmount;
        Debug.Log("Bandit - Magenta! HP: " + Health);
        if (Health <= 0)
        {
            //Destroy(this.gameObject);
            this.CancelInvoke();
            Die();
        }
    }

    public override void Die()
    {
        Health = 10;
        GetComponent<MeshRenderer>().material.color = defaultColor;
        Debug.Log("Bandit Dying");
        this.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        Health = 10;
        V0SpawnManager.enemyCount++;
        //_ui = GameObject.Find("UI Manager").GetComponent<V0UIManager>();
        //_ui.UpdateEnemyCount();
        V0UIManager.Instance.UpdateEnemyCount();
        Invoke("Die", Random.Range(2, 6));
        defaultColor = GetComponent<MeshRenderer>().material.color;
    }

    public void OnDisable()
    {
        V0SpawnManager.enemyCount--;
        //_ui.UpdateEnemyCount();
        V0UIManager.Instance.UpdateEnemyCount();
    }
}
