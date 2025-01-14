using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public GameObject pauseMenu;
    
    //Use this if we are working with different scenes
    public void GoToScene (string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }

    //Use this if we are working with panels
    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
