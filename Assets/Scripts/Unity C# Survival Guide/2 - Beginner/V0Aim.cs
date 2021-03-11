using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0Aim : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private int _aimSpeed = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //direction to face = destination - source
        Vector3 toFace = _target.position - transform.position;
        Debug.DrawRay(transform.position, toFace, Color.red);

        //access current rotation = quaternion look rotation
        Quaternion targetRotation = Quaternion.LookRotation(toFace);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*_aimSpeed);
    }
}
