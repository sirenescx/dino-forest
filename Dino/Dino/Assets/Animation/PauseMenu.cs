using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за приостановление игрового процесса (паузу).
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Панель, содержащая в себе меню паузы.
    /// </summary>
    public GameObject menuPanel;
    /// <summary>
    /// Поставлена игра на паузу или нет.
    /// </summary>
    private bool isPaused = false;
    /// <summary>
    /// Панель, содержащая в себе меню выбора действий после смерти персонажа.
    /// </summary>
    public GameObject deathMenuPanel;
    /// <summary>
    /// Компонент, содержащий в себе информацию о монетках.
    /// </summary>
    public GameObject coinText;
    /// <summary>
    /// Изображение шкалы здоровья.
    /// </summary>
    public Image heartsImage;
    /// <summary>
    /// Компонент для отображения текстовой информации на экран.
    /// </summary>
    public Text scoreText;

    private void Start()
    {
        menuPanel.SetActive(false);
        deathMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (EndLevelMenu.isEnded)
        {
            menuPanel.SetActive(false);
            deathMenuPanel.SetActive(false);
            return;
        }
        else
        {
            if (Input.GetButtonDown("Pause"))
            {
                isPaused = !isPaused;
                deathMenuPanel.SetActive(false);
            }

            // Если игра поставлена на паузу:
            if (isPaused & !Dino.isDead)
            {
                // deathMenuPanel.SetActive(false);
                // Меню паузы отображается.
                menuPanel.SetActive(true);
                // Остановка игрового времени (пауза).
                Time.timeScale = 0;
            }
            // Если игра не поставлена на паузу:
            else
            {
                // Меню паузы не отображается.
                menuPanel.SetActive(false);
                // Возвращение игрового времени.
                Time.timeScale = 1;
                if (Dino.isDead)
                {
                    menuPanel.SetActive(false);
                    // Меню отображается.
                    deathMenuPanel.SetActive(true);
                    // Остановка игрового времени (пауза).
                    Time.timeScale = 0;
                    heartsImage.enabled = false;
                    coinText.SetActive(false);
                }
                else
                {
                    // Меню не отображается.
                    deathMenuPanel.SetActive(false);
                    // Возвращение игрового времени.
                    Time.timeScale = 1;
                }
            }
        }
    }
    /// <summary>
    /// Метод для отображения счета на экран.
    /// </summary>
    private void OnGUI()
    {
        //Цвет текста надписи.
        scoreText.color = Color.white;
        int score = CoinPick.coinCounter;
        for (int i = 0; i <= score; i++)
        {
            //Текст надписи.
            scoreText.text = "Score: " + i.ToString();
        }
    }
    /// <summary>
    /// Метод, возвращающий игру из паузы в режим игры по нажатию кнопки Resume.
    /// </summary>
    public void Resume()
    {
        isPaused = false;
    }
    /// <summary>
    /// Метод, начинающий уровень заново по нажатию кнопки Restart.
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
