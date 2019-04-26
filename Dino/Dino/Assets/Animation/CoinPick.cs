using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс для сбора монеток.
/// </summary>
public class CoinPick : MonoBehaviour
{
    /// <summary>
    /// Персонаж.
    /// </summary>
    public Dino dino;
    /// <summary>
    /// Счетчик монеток.
    /// </summary>
    public static int coinCounter;
    /// <summary>
    /// Компонент для отображения текстовой информации на экран.
    /// </summary>
    public Text countText;
    /// <summary>
    /// Источник аудио.
    /// </summary>
    private AudioSource source;
    /// <summary>
    /// Звук собирания монетки.
    /// </summary>
    public AudioClip coinPickSound;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    /// <summary>
    /// Метод, уничтожающий объект при столкновении с персонажем и добавляющий персонажу 1 очко за это действие.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            coinCounter += 1;
            Destroy(other.gameObject);
            source.PlayOneShot(coinPickSound);
        }
    }
    /// <summary>
    /// Метод для отображения счета на экран.
    /// </summary>
    private void OnGUI()
    {
        //Цвет текста надписи.
        countText.color = Color.white;
        //Текст надписи.
        countText.text = "Coins: " + coinCounter.ToString();
    }
}

