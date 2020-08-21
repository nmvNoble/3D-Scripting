using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public static int enemyCount;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            Instantiate(enemyPrefab, 
                new Vector3(Random.Range(-7,7), Random.Range(0, 7), 7), 
                Quaternion.identity);
        }
    }
}
