using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс движущейся платформы.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    /// <summary>
    /// Объект движущейся платформы.
    /// </summary>
    public static GameObject movingPlatform;
    /// <summary>
    /// Массив точек, между которыми перемещается платформа.
    /// </summary>
    public Transform[] points;
    /// <summary>
    /// Номер начальной точки движения.
    /// </summary>
    public int startPoint;
    /// <summary>
    /// Номер конечной точки движения.
    /// </summary>
    public int targetPoint;
    /// <summary>
    /// Скорость перемещения платформы.
    /// </summary>
    public float speed;
    /// <summary>
    /// Физическое тело движущейся платформы.
    /// </summary>
    public Rigidbody2D platformRgdBd2D;

    /// <summary>
    /// Инициализация физического тела платформы и установка платформы на начальную точку.
    /// </summary>
    void Start()
    {
        platformRgdBd2D = GetComponent<Rigidbody2D>();
        transform.position = points[startPoint].position;
    }

    /// <summary>
    /// Перемещение платформы с течением времени.
    /// </summary>
    void FixedUpdate()
    {
        platformRgdBd2D.MovePosition(Vector2.MoveTowards(transform.position, points[targetPoint].position, speed * Time.deltaTime));

        if (transform.position == points[targetPoint].position)
        {
            targetPoint++;
            if (targetPoint == points.Length)
            {
                targetPoint = 0;
            }
        }
    }
}