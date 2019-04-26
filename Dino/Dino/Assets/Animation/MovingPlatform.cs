using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public static GameObject movingPlatform;

    public Transform[] point;
    public int startPoint;
    public int targetPoint;
    public float speed;
    public Rigidbody2D platformRgdBd2D;
    //public Transform platformTransform;
    // Start is called before the first frame update
    void Start()
    {
        platformRgdBd2D = GetComponent<Rigidbody2D>();

        //gameObject.
        transform.position = point[startPoint].position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        platformRgdBd2D.MovePosition(Vector2.MoveTowards(transform.position, point[targetPoint].position, speed * Time.deltaTime));
        
        // gameObject.
      //  transform.position = Vector2.MoveTowards(transform.position, point[targetPoint].position, speed * Time.deltaTime);
        //platformTransform.position = Vector2.MoveTowards(platformTransform.position, point[targetPoint].position, speed * Time.deltaTime);
        if (transform.position == point[targetPoint].position)
        {
            targetPoint++;
            if (targetPoint == point.Length)
            {
                targetPoint = 0;
            }
        }
    }
}
