using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Класс меню конца уровня.
/// </summary>
public class EndLevelMenu : MonoBehaviour
{
    /// <summary>
    /// Поле, содержащее информацию о том, был ли пройден уровень.
    /// </summary>
    public static bool isEnded;
    /// <summary>
    /// Поле, содержащее информацию о том, отображается ли меню паузы.
    /// </summary>
    public static bool pauseMenuDisabled;
    /// <summary>
    /// Панель, содержащая в себе меню конца уровня.
    /// </summary>
    public GameObject endLevelMenuPanel;
    /// <summary>
    /// Надпись о прохождении уровня.
    /// </summary>
    public Text levelNumberText;
    /// <summary>
    /// Изображение шкалы здоровья.
    /// </summary>
    public Image heartImage;
    /// <summary>
    /// Компонент, содержащий в себе информацию о монетках.
    /// </summary>
    public GameObject coinInfo;
    /// <summary>
    /// Является ли уровень обучающим.
    /// </summary>
    public bool isTutorial;

    /// <summary>
    /// Скрытие меню конца уровня, задание текста, содержащего информацию о том, что текущий уровень был пройден.
    /// </summary>
    void Start()
    {
        Time.timeScale = 1;
        endLevelMenuPanel.SetActive(false);
        if (!isTutorial)
        levelNumberText.text = $"Level {int.Parse(SceneManager.GetActiveScene().name.Substring(5, 1))} Finished";
        else
            levelNumberText.text = $"Tutorial Finished";
    }

    /// <summary>
    /// Обновление информации о том, был ли пройден уровень с течением времени.
    /// </summary>
    void Update()
    {
        if (isEnded)
        {
            endLevelMenuPanel.SetActive(true);
            Time.timeScale = 0;
            heartImage.enabled = false;
            coinInfo.SetActive(false);
        }
        else
        {
            endLevelMenuPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Метод для перезапуска уровня.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Метод для перехода к следующему уровню.
    /// </summary>
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Метод для перехода к основному игровому меню.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Метод для выхода из программы.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}