using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelMenu : MonoBehaviour
{
    public static bool isEnded;
    public static bool pauseMenuDisabled;
    /// <summary>
    /// Панель, содержащая в себе меню паузы.
    /// </summary>
    public GameObject endLevelMenuPanel;
    /// <summary>
    /// Надпись о прохождении уровня.
    /// </summary>
    public Text levelNumberText;
    /// <summary>
    /// Изображение шкалы здоровья.
    /// </summary>
    public Image heartsImage;
    /// <summary>
    /// Компонент, содержащий в себе информацию о монетках.
    /// </summary>
    public GameObject coinText;
    void Start()
    {
        Time.timeScale = 1;
        endLevelMenuPanel.SetActive(false);
        levelNumberText.text = $"Level {int.Parse(SceneManager.GetActiveScene().name.Substring(5, 1))} Finished";
    }

    void Update()
    {
        if (isEnded)
        {
            endLevelMenuPanel.SetActive(true);
            Time.timeScale = 0;
            heartsImage.enabled = false;
            coinText.SetActive(false);
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

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
