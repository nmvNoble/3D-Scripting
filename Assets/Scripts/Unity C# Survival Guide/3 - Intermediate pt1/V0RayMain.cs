using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0RayMain : MonoBehaviour
{
    public V0Wizard wiz = new V0Wizard();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(rayOrigin, out hitInfo))
            {
                V0IDamagable obj = hitInfo.collider.GetComponent<V0IDamagable>();
                //hitInfo.collider.GetComponent<V0Bandit>().Damage(1);
                if (obj != null)
                {
                    wiz.Cast();
                    obj.Damage(2);
                }
                
            }
        }
    }
}
