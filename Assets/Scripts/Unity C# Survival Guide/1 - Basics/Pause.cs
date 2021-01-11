using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Text PauseBtn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleTime();
    }

    public void ToggleTime()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            PauseBtn.text = "Resume";
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            PauseBtn.text = "Pause";
        }
    }
}
