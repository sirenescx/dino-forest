using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// Класс меню для выбора из списка существующих уровней.
/// </summary>
public class ChooseLevel : MonoBehaviour
{
    /// <summary>
    /// Кнопки перехода к уровням.
    /// </summary>
    public Button[] levelButtons;
    /// <summary>
    /// Количество открытых уровней.
    /// </summary>
    int openedLevels;
    /// <summary>
    ///  Изображения замков для закрытых уровней.
    /// </summary>
    public Image[] locks;
    /// <summary>
    /// Изображения кнопок.
    /// </summary>
    public Image[] buttonImages;

    void Start()
    {
        // Получение количества пройденных уровней.
        openedLevels = PlayerPrefs.GetInt("openedLevels");

        // Отображение открытых уровней.
        try
        {
            for (int i = 1; i <= openedLevels; i++)
            {
                levelButtons[i].enabled = true;
                locks[i - 1].enabled = false;
                buttonImages[i - 1].enabled = true;
            }
        }
        catch (IndexOutOfRangeException) { }

        // Отображение закрытых уровней.
        for (int i = openedLevels + 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].enabled = false;
            locks[i - 1].enabled = true;
            buttonImages[i - 1].enabled = false;
        }
    }

    /// <summary>
    /// Метод перехода к уровню с заданным номером.
    /// </summary>
    /// <param name="levelNumber"></param>
    public void Level(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber + 1);
    }


    /// <summary>
    /// Метод перехода к основному игровому меню.
    /// </summary>
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}