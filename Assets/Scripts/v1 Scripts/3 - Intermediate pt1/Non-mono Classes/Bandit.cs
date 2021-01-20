using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy, IDamagable
{
    //private UIManager _ui;

    public int Health { get; set; }
    private Color defaultColor;

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public Vector3 RetPos()
    {
        return transform.position;
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
        SpawnManager.enemyCount++;
        //_ui = GameObject.Find("UI Manager").GetComponent<UIManager>();
        //_ui.UpdateEnemyCount();
        UIManager.Instance.UpdateEnemyCount();
        Invoke("Die", Random.Range(2, 6));
        defaultColor = GetComponent<MeshRenderer>().material.color;
    }

    public void OnDisable()
    {
        SpawnManager.enemyCount--;
        //_ui.UpdateEnemyCount();
        UIManager.Instance.UpdateEnemyCount();
    }
}
