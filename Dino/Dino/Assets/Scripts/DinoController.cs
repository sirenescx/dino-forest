using UnityEngine;
using System.Collections;

public class DinoController : MonoBehaviour
{
    /// <summary>
    /// Персонаж.
    /// </summary>
    public Dino dino;

    /// <summary>
    /// Метод, вызываемый в начале игры. Инициализация персонажа и его физического тела.
    /// </summary>
    void Start()
    {
        dino = dino ?? GetComponent<Dino>();
        dino.dinoRgdBd2D = GetComponent<Rigidbody2D>();
        if (dino == null)
        {
            Debug.LogError("Player not set to controller");
        }
    }

    /// <summary>
    /// Метод для управления передвижением персонажа с помощью клавиатуры и обновления этой информации с течением времени.
    /// </summary>
    void Update()
    {
        if (dino != null)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                dino.MoveRight();
            }

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                dino.Jump();
            }


            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                dino.MoveLeft();
            }

            if (dino.dinoRgdBd2D.position.y < -7)
            {
                dino.Die();
            }
        }
    }
}
