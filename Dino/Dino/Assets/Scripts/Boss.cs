#pragma warning disable 0649
using UnityEngine;
using System.Collections;
using System;


class Boss : MonoBehaviour
{
    /// <summary>
    /// Физический объект тела вражеского персонажа.
    /// </summary>
    public Rigidbody2D bossRgdB2D;
    /// <summary>
    /// Массив точек, между которыми перемещается персонаж.
    /// </summary>
    public Transform[] point;
    /// <summary>
    /// Начальная точка движения.
    /// </summary>
    public int startPoint = 0;
    /// <summary>
    /// Конечная точка движения.
    /// </summary>
    public int targetPoint;
    /// <summary>
    /// Скорость перемещения.
    /// </summary>
    float speed = 4;
    /// <summary>
    /// Контроллер персонажа.
    /// </summary>
    public Animator bossController;
    /// <summary>
    /// Количество жизненей врага.
    /// </summary>
    public static int lives = 10;
    /// <summary>
    /// Рандомайзер для генерации номера следующей точки.
    /// </summary>
    public static System.Random generateRandomPoint = new System.Random();

    /// <summary>
    /// Инициализация физического тела персонажа, установка позиции персонажа на позицию начальной точки.
    /// </summary>
    void Start()
    {
        lives = 10;
        bossRgdB2D = GetComponent<Rigidbody2D>();
        transform.position = point[startPoint].position;
    }

    /// <summary>
    /// Изменение позиции персонажа с течением времени.
    /// </summary>
    void FixedUpdate()
    {
        bossRgdB2D.MovePosition(Vector2.MoveTowards(transform.position, point[targetPoint].position, speed * Time.deltaTime));

        if (transform.position == point[targetPoint].position)
        {
            int currentPoint = -1;

            if (targetPoint == 2 || targetPoint == 1)
            {
                bossController.SetInteger("bossDirection", -1);
            }
            else
            {
                bossController.SetInteger("bossDirection", 1);
            }

            currentPoint = targetPoint;
            
            while (true)
            {
                targetPoint = generateRandomPoint.Next(0, point.Length);
                
                if (targetPoint != currentPoint) 
                { 
                    break;
                }
            }

            if (targetPoint == point.Length)
            {
                bossController.SetInteger("bossDirection", 1);
                targetPoint = 0;
            }
        }
    }

    /// <summary>
    /// Метод для запуска таймера после получения боссом урона, во время которого он больше не может получать урон. Осуществление анимации мигания при получении удара.
    /// </summary>
    /// <returns></returns>
    public static IEnumerator BossUndamagedTimer()
    {
        try
        {
            string hitState = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().GetInteger("bossDirection") == 1 
                ? "BossHitRight" 
                : "BossHitLeft";
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().CrossFadeInFixedTime(hitState, 0.5f);
        } catch (NullReferenceException) { }
        
        yield return new WaitForSeconds(0.75f);
        
        try
        {
            string afterHitState = GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().GetInteger("bossDirection") == 1 
                ? "BossRight" 
                : "BossLeft";
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().Play(afterHitState);
        } catch (NullReferenceException) { }
    }

    /// <summary>
    /// Метод для установки количества времени отображения неигрового-персонажа босса после его смерти.
    /// </summary>
    /// <returns></returns>
    public static IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(2f);
    }
}
