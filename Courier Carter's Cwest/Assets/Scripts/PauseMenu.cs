using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;

    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
            if(Paused)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                Paused = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                Paused = true;
            }
    }
}
