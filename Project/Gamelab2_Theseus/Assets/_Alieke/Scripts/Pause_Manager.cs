using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Manager : MonoBehaviour
{
    public bool paused;
    public GameObject pausePanel;
    public GameObject optionsPanel;
	
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (paused)
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                paused = false;
            }
            else
            {        
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                paused = true;
            }
        }
	}

    public void ContinueGameButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        paused = false;
    }

    public void PauseMenuButton()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void OptionsButton()
    {
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }
}
