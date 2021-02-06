using System.Collections;
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
    public int RedCounter = 0, GreenCounter = 0, BlueCounter = 0, four =0;

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
                bandit.transform.position = new Vector3(Random.Range(-7, 7), Random.Range(0, 7), 7);
                bandit.SetActive(true);
                switch ((int)Random.Range(1, 4))
                {
                    case 1:
                        //RedCounter++;
                        UtilityHelper.ChangeColor(bandit, Color.red);
                        break;
                    case 2:
                        //GreenCounter++;
                        UtilityHelper.ChangeColor(bandit, Color.green);
                        break;
                    case 3:
                        //BlueCounter++;
                        UtilityHelper.ChangeColor(bandit, Color.blue);
                        break;
                }
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
