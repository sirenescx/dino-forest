using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoHealth : MonoBehaviour
{
    /// <summary>
    /// Массив изображений шкалы здоровья.
    /// </summary>
    public Sprite[] hearts;
    /// <summary>
    /// Изображение шкалы здоровья на панели.
    /// </summary>
    public Image heartImage;
    /// <summary>
    /// Персонаж.
    /// </summary>
    Dino dino;

    /// <summary>
    /// Инициализация персонажа. 
    /// </summary>
    void Start()
    {
        dino = GameObject.FindGameObjectWithTag("Player").GetComponent<Dino>();
    }

    /// <summary>
    /// Обновление шкалы здоровья с течением времени.
    /// </summary>
    void Update()
    {
        if (!Dino.isDead & dino.lostLives <= 5)
        {
            heartImage.sprite = hearts[dino.lostLives];
        }
    }
}
