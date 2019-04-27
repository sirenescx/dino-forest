using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DinoStarsScale : MonoBehaviour
{
    public Sprite[] stars = new Sprite[4];
    public Image starImage;
    int levelCoinAmount;
    void Start()
    {
        levelCoinAmount = int.Parse(SceneManager.GetActiveScene().name.Substring(5, 1)) * 6;
    }

    void Update()
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
