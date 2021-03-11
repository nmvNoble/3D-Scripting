using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V0UserInput : MonoBehaviour
{
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

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.collider.tag == "Command Pattern")
                {
                    //UtilityHelper.ChangeColor(hitInfo.collider.gameObject, Color.grey, true);
                    V0ICommand click = new V0ClickCommand(hitInfo.collider.gameObject, Color.grey, true);
                    click.Execute();
                    V0CommandManager.Instance.AddCommand(click);
                }

            }
        }
    }
}
