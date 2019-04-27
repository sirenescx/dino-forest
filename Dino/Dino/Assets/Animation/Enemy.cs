using UnityEngine;

/// <summary>
/// Класс для перемещения вражеского персонажа.
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Вражеский персонаж.
    /// </summary>
    public Enemy enemy;
    /// <summary>
    /// Физический объект тела вражеского персонажа.
    /// </summary>
    public Rigidbody2D enemyRgdB2D;
    /// <summary>
    /// Массив точек, между которыми перемещается персонаж.
    /// </summary>
    public Transform[] point;
    /// <summary>
    /// Начальная точка движения.
    /// </summary>
    public int startPoint;
    /// <summary>
    /// Конечная точка движения.
    /// </summary>
    public int targetPoint;
    /// <summary>
    /// Скорость перемещения.
    /// </summary>
    public float speed;
    /// <summary>
    /// Контроллер персонажа.
    /// </summary>
    public Animator enemyController;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyRgdB2D = GetComponent<Rigidbody2D>();
        transform.position = point[startPoint].position;
    }

    void FixedUpdate()
    {
        enemyRgdB2D.MovePosition(Vector2.MoveTowards(transform.position, point[targetPoint].position, speed * Time.deltaTime));

        if (transform.position == point[targetPoint].position)
        {
            enemyController.SetInteger("direction", 1);
            targetPoint++;

            if (targetPoint == point.Length)
            {
                enemyController.SetInteger("direction", -1);
                targetPoint = 0;
            }
        }
    }
}

