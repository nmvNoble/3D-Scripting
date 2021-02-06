using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Pause : MonoBehaviour
{
    [SerializeField] private Text PauseBtn;
    [SerializeField] private GameObject PauseMenu;

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
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            PauseBtn.text = "Resume";
        }
        else if (Time.timeScale == 0)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            PauseBtn.text = "Pause";
        }
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
    }
}
