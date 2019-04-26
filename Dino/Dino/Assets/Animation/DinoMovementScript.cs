using UnityEngine;
using System.Collections;

public class DinoMovementScript : MonoBehaviour
{

    public Dino dino;

    private void Start()
    {
        dino = dino ?? GetComponent<Dino>();
        dino.dinoRgdBd2D = GetComponent<Rigidbody2D>();
        if (dino == null)
        {
            Debug.LogError("Player not set to controller");
        }
    }

    private void Update()
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

            if (dino.dinoRgdBd2D.position.y < -7)//-5.73)
            {
                dino.Die();
            }
        }
    }
}
