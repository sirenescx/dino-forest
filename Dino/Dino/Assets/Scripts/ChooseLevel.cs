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

    void Update()
    {
        
    }

    public void Level1()
    {
        SceneManager.LoadScene(2);
    }
    /// <summary>
    /// Метод, начинающий уровень заново по нажатию кнопки Restart.
    /// </summary>
    public void Level2()
    {
        SceneManager.LoadScene(3);
    }

    public void Level3()
    {
        SceneManager.LoadScene(4);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
