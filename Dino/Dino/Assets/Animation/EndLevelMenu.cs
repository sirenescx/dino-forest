using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMenu : MonoBehaviour
{
    public static bool isEnded;
    public static bool pauseMenuDisabled;
    /// <summary>
    /// Панель, содержащая в себе меню паузы.
    /// </summary>
    public GameObject endLevelMenuPanel;
    void Start()
    {
        Time.timeScale = 1;
        endLevelMenuPanel.SetActive(false);
    }

    void Update()
    {
        if (isEnded)
        {
            endLevelMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            endLevelMenuPanel.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
