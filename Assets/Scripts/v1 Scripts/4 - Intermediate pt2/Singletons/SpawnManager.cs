﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public override void Init()
    {
        base.Init();
    }

    [SerializeField]
    private GameObject _banditContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private List<GameObject> _banditPool;

    public static int enemyCount;
    public bool isGameOver = false;
    public int time;

    // Start is called before the first frame update
    void Start()
    {
        _banditPool = GenerateBandits(10);
        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator StartTimer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1.0f);
            time++;
            if (time%3 == 0)
            {
                GameObject bandit = spawnBandit();
                bandit.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(0, 7), 7);
            }
            //_timeText.text = "Time: " + time;
        }
    }

    List<GameObject> GenerateBandits(int amount)
    {
        for (int i=0; i<amount; i++)
        {
            GameObject bandit = Instantiate(_enemyPrefab,
                new Vector3(Random.Range(-7, 7), Random.Range(0, 7), 7),
                Quaternion.identity);
            bandit.transform.parent = _banditContainer.transform;
            bandit.SetActive(false);
            _banditPool.Add(bandit);
        }
        
        return _banditPool;
    }

    public GameObject spawnBandit()
    {
        foreach(var bandit in _banditPool)
        {
            if(bandit.activeInHierarchy == false)
            {
                bandit.SetActive(true);
                return bandit;
            }
        }
        var newBandit = Instantiate(_enemyPrefab,
                new Vector3(Random.Range(-7, 7), Random.Range(0, 7), 7),
                Quaternion.identity);
        newBandit.transform.parent = _banditContainer.transform;
        _banditPool.Add(newBandit);
        return newBandit;
    }


}
