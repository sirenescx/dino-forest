using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс основного меню.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Метод перехода к меню выбора уровня.
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Метод перехода к меню настроек.
    /// </summary>
    public void Settings()
    {
        SceneManager.LoadScene(5);
    }

    /// <summary>
    /// Метод перехода к обучению.
    /// </summary>
    public void Tutorial()
    {
        SceneManager.LoadScene(6);
    }

    /// <summary>
    /// Метод выхода из программы.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Метод для сброса прогресса в игре.
    /// </summary>
    public void RestartGame()
    {
        PlayerPrefs.SetInt("openedLevels", 0);
    }
}