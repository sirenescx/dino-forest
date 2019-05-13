﻿using UnityEngine;
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
    public Image[] buttonImage;

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
                buttonImage[i - 1].enabled = true;
            }
        }
        catch (IndexOutOfRangeException) { }

        // Отображение закрытых уровней.
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
