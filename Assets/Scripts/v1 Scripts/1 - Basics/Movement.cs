using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private float horizontalInput, verticalInput;

    ICommand moveHorizontal, moveVertical;

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            moveHorizontal = new MoveHorizontalCommand(this.transform, speed, horizontalInput);
            moveHorizontal.Execute();
            //CommandManager.Instance.AddCommand(moveHorizontal);
        }
        


        if (Input.GetAxis("Vertical") != 0)
        {
            float verticalInput = Input.GetAxis("Vertical");
            moveVertical = new MoveVerticalCommand(this.transform, speed, verticalInput);
            moveVertical.Execute();
            //CommandManager.Instance.AddCommand(moveVertical);
        }
        
    }
}
