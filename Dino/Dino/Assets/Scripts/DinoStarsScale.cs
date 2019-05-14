using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DinoStarsScale : MonoBehaviour
{
    /// <summary>
    /// Массив изображений шкалы звезд.
    /// </summary>
    public Sprite[] stars = new Sprite[4];
    /// <summary>
    /// Изображение шкалы звезд на панели.
    /// </summary>
    public Image starImage;
    /// <summary>
    /// Количество монет, которые существует на текущем уровне.
    /// </summary>
    int levelCoinAmount;
    /// <summary>
    /// Является ли уровень обучающим.
    /// </summary>
    public bool isTutorial;

    /// <summary>
    /// Установка количества монет в соответствии с текущим уровнем.
    /// </summary>
    void Start()
    {
        if (!isTutorial)
            levelCoinAmount = int.Parse(SceneManager.GetActiveScene().name.Substring(5, 1)) * 6;
        else return;
    }

    /// <summary>
    /// Обновление шкалы звезд с течением времени.
    /// </summary>
    void Update()
    {
        if (isTutorial) return;
        else
        {
            if (CoinPick.coinCounter <= levelCoinAmount / 4f)
                starImage.sprite = stars[3];
            if (CoinPick.coinCounter > levelCoinAmount / 4f & CoinPick.coinCounter <= levelCoinAmount / 2f)
                starImage.sprite = stars[2];
            if (CoinPick.coinCounter > levelCoinAmount / 2f & CoinPick.coinCounter < levelCoinAmount * 3f / 4)
                starImage.sprite = stars[1];
            if (CoinPick.coinCounter > levelCoinAmount * 3f / 4)
                starImage.sprite = stars[0];
        }
    }
}
