using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public override void Init()
    {
        base.Init();
    }

    public int t1Count, t2Count, t3Count;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private List<GameObject> _enemyPool;

    public static int enemyCount;
    public bool isGameOver = false;
    public int time, wave, type;
    public int RedCounter = 0, GreenCounter = 0, BlueCounter = 0, enemyColorCounter = 0, randColor = 0;

    // Start is called before the first frame update
    void Start()
    {
        _enemyPool = GenerateEnemies(10);
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
            if (time%1 == 0)
            {
                GameObject enemy = spawnEnemy();
                //enemy.GetComponent<Enemy>().SetEnemyType(Random.Range(1, 4));
                enemy.SetActive(true);
            }
            //_timeText.text = "Time: " + time;
        }
    }

    List<GameObject> GenerateEnemies(int amount)
    {
        for (int i=0; i<amount; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab,
                new Vector3(Random.Range(-6, 6), 2, 7),
                Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            ChangeColor(enemy);
            _enemyPool.Add(enemy);
        }
        
        return _enemyPool;
    }

    public GameObject spawnEnemy()
    {
        foreach(var enemy in _enemyPool)
        {
            if(enemy.activeInHierarchy == false && !enemy.GetComponent<Enemy>().isBugged)
            {
                ChangeColor(enemy);
                enemy.transform.position = new Vector3(Random.Range(-6, 6), 2, 7);
                return enemy;
            }
        }
        var newEnemy = Instantiate(_enemyPrefab,
                new Vector3(Random.Range(-7, 7), Random.Range(0, 7), 7),
                Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        _enemyPool.Add(newEnemy);
        return newEnemy;
    }
    public int DetermineEnemyType()
    {
        type = Random.Range(1, 4);
        if (type == 1 && t1Count > (t2Count+2) && t1Count > (t3Count + 2))
            if (t2Count < t3Count)
                type = 2;
            else
                type = 3;

        else if (type == 2 && t2Count > (t1Count + 2) && t2Count > (t3Count + 2))
            if (t1Count < t3Count)
                type = 1;
            else
                type = 3;

        else if (type == 3 && t3Count > (t1Count + 2) && t3Count > (t2Count + 2))
            if (t1Count < t2Count)
                type = 1;
            else
                type = 2;
        return type;
    }

    private void ChangeColor(GameObject enemy)
    {
        do
        {
            randColor = (int)Random.Range(1, 4);
        }
        while (randColor == enemyColorCounter);
        switch (randColor)
        {
            case 1:
                //RedCounter++;
                enemyColorCounter = 1;
                UtilityHelper.ChangeColor(enemy, Color.red);
                break;
            case 2:
                //GreenCounter++;
                enemyColorCounter = 2;
                UtilityHelper.ChangeColor(enemy, Color.green);
                break;
            case 3:
                //BlueCounter++;
                enemyColorCounter = 3;
                UtilityHelper.ChangeColor(enemy, Color.blue);
                break;
        }
    }

    public void ResetEnemies()
    {
        foreach (var enemy in _enemyPool)
        {
            enemy.SetActive(false);
        }
    }

}
