using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void RestartGame()
    {
        PlayerPrefs.SetInt("openedLevels", 0);
    }
}
