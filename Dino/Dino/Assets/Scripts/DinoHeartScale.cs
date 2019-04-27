using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoHeartScale : MonoBehaviour
{
    public Sprite[] hearts;
    public Image heartImage;
    Dino dino;
    void Start()
    {
        dino = GameObject.FindGameObjectWithTag("Player").GetComponent<Dino>();
    }

    void Update()
    {
        if (!Dino.isDead & dino.lostLives <= 5)
        {
            heartImage.sprite = hearts[dino.lostLives];
        }
    }
}
