using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMain : MonoBehaviour
{
    public Wizard wiz = new Wizard();
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
                IDamagable obj = hitInfo.collider.GetComponent<IDamagable>();
                //hitInfo.collider.GetComponent<Bandit>().Damage(1);
                if (obj != null)
                {
                    wiz.Cast();
                    obj.Damage(2);
                }
                
            }
        }
    }
}
