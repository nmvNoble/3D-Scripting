using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Vector3[] positions;
    private int _randIndex, _currIndex = 1;

    public GameObject[] playerBoxes;

    // Start is called before the first frame update
    void Start()
    {
        playerBoxes = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandPos();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UtilityHelper.SetPosToZero(this.gameObject);
        }
    }

    void RandPos()
    {
        _randIndex = GetRand();

        while (_randIndex == _currIndex)
        {
            _randIndex = GetRand();
        }

        UtilityHelper.ChangeColor(playerBoxes[_randIndex+1], Color.grey, true);//index +1 because target is also player tag
        transform.position = GetPosition(_randIndex);
        _currIndex = _randIndex;

    }

    int GetRand()
    {
        return Random.Range(0, positions.Length);
    }

    Vector3 GetPosition(int index)
    {
        return positions[index];
    }
}
