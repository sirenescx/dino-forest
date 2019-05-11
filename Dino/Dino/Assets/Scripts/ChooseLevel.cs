using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Класс меню для выбора из списка существующих уровней.
/// </summary>
public class ChooseLevel : MonoBehaviour
{
    public Button[] levelButtons;
    int openedLevels;
    public Image[] locks;
    public Image[] buttonImage;

    void Start()
    {
        openedLevels = PlayerPrefs.GetInt("openedLevels");

        for (int i = 1; i <= openedLevels; i++)
        {
            levelButtons[i].enabled = true;
            locks[i - 1].enabled = false;
            buttonImage[i - 1].enabled = true;

        }
        for (int i = openedLevels + 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].enabled = false;
            locks[i - 1].enabled = true;
            buttonImage[i - 1].enabled = false;
        }

    } 

    /// <summary>
    /// Метод перехода к первому уровню.
    /// </summary>
    public void Level1()
    {
        SceneManager.LoadScene(2);
    }
    /// <summary>
    /// Метод перехода ко второму уровню.
    /// </summary>
    public void Level2()
    {
        SceneManager.LoadScene(3);
    }

    /// <summary>
    /// Метод перехода к третьему уровню.
    /// </summary>
    public void Level3()
    {
        SceneManager.LoadScene(4);
    }

    /// <summary>
    /// Метод перехода к основному игровому меню.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
