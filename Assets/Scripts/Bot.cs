using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Vector3[] positions;
    private int _randIndex, _currIndex = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _randIndex = GetRand();

            while (_randIndex == _currIndex)
            {
                _randIndex = GetRand();
            }

            transform.position = GetPosition(_randIndex);
            _currIndex = _randIndex;
        }
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
