using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Метод, начинающий уровень заново по нажатию кнопки Restart.
    /// </summary>
    public void Level2()
    {
        SceneManager.LoadScene(1);
    }

    public void Level3()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(3);
    }
}
