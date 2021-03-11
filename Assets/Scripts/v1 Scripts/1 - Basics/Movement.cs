using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private float _horizontalInput;
    private float _verticalInput;

    ICommand moveHorizontal, moveVertical;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)//Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            float _horizontalInput = Input.GetAxis("Horizontal");
            moveHorizontal = new MoveHorizontalCommand(this.transform, _speed, _horizontalInput);
            moveHorizontal.Execute();
            //CommandManager.Instance.AddCommand(moveHorizontal);
        }
        


        if (Input.GetAxis("Vertical") != 0)//Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            float _verticalInput = Input.GetAxis("Vertical");
            moveVertical = new MoveVerticalCommand(this.transform, _speed, _verticalInput);
            moveVertical.Execute();
            //CommandManager.Instance.AddCommand(moveVertical);
        }
        
    }
}
