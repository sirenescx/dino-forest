using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс меню для выбора из списка существующих уровней.
/// </summary>
public class ChooseLevel : MonoBehaviour
{
    void Start()
    {
        
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
