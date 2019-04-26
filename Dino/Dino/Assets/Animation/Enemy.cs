using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Enemy enemy;
    public Rigidbody2D enemyRgdB2D;
    public Transform[] point;
    public int startPoint;
    public int targetPoint;
    public float speed;
    public MoveState moveState;
    public Animator enemyController;
    public enum MoveState
    {
        EnemyWalkRight, EnemyWalkLeft
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyRgdB2D = GetComponent<Rigidbody2D>();
        transform.position = point[startPoint].position;
        
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        enemyRgdB2D.MovePosition(Vector2.MoveTowards(transform.position, point[targetPoint].position, speed * Time.deltaTime));

        if (transform.position == point[targetPoint].position)
        {
            targetPoint++;
            moveState = MoveState.EnemyWalkRight;
            enemyController.Play("EnemyWalkRight");
            if (targetPoint == point.Length)
            {
                targetPoint = 0;
                moveState = MoveState.EnemyWalkLeft;
               enemyController.Play("EnemyWalkLeft");
            }
        }
    }
}

