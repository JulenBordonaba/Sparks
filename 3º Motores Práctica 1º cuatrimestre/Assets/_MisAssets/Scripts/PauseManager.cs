using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool inPause;

    public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        inPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(inPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Pause()
    {
        inPause = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        inPause = false;
    }
}
