﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Timers;
using System.Collections;

/// <summary>
/// Класс игрока.
/// </summary>
public class Dino : MonoBehaviour
{
    /// <summary>
    /// Скорость передвижения игрока.
    /// </summary>
    public float walkSpeed = 2;
    /// <summary>
    /// Сила прыжка игрока.
    /// </summary>
    public float jumpForce = 5;
    /// <summary>
    /// Счетчик потерянных жизней у игрока.
    /// </summary>
    public int lostLives = 0;
    /// <summary>
    /// Поле для отображения информации о количестве монет у игрока.
    /// </summary>
    public Text countText;
    /// <summary>
    /// Контейнер для взаимодействия игрока 
    /// </summary>
    public Rigidbody2D dinoRgdBd2D;
    /// <summary>
    /// Контроллер игрока.
    /// </summary>
    private Animator animatorController;
    /// <summary>
    /// Поле, имеющее значение true, если игрок находится на твердой поверхности, иначе - false.
    /// </summary>
    bool isGrounded;
    /// <summary>
    /// Компонент для проверки поверхности под игроком.
    /// </summary>
    public Transform groundCheck;
    /// <summary>
    /// Поле, содержащее информацию о названии слоя-земли.
    /// </summary>
    public LayerMask whatIsGround;
    /// <summary>
    /// Радиус соприкосновения с землей.
    /// </summary
    readonly float groundRadius = 0.5f;
    /// <summary>
    /// Переменная, отвечающая за направление движения: имееет значение -1 при движении влево, и 1 при движении вправо.
    /// </summary>
    int direction;
    /// <summary>
    /// Источник аудио.
    /// </summary>
    private AudioSource source;
    /// <summary>
    /// Звук убийства врага.
    /// </summary>
    public AudioClip enemyHitSound;
    /// <summary>
    /// Звук получения урона.
    /// </summary>
    public AudioClip heroHitSound;
    /// <summary>
    /// Жив персонаж или нет.
    /// </summary>
    public static bool isDead;
    /// <summary>
    /// 
    /// </summary>
    bool wasHit = false;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        dinoRgdBd2D = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        isDead = false;
        CoinPick.coinCounter = 0;
        EndLevelMenu.isEnded = false;
    }

    /// <summary>
    /// Метод, отвечающий за прыжок игрока.
    /// </summary>
    public void Jump()
    {
        if (isGrounded & !animatorController.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            dinoRgdBd2D.velocity = new Vector2(dinoRgdBd2D.velocity.x, jumpForce);
        }
        isGrounded = false;
        animatorController.SetBool("isGrounded", isGrounded);
    }

    /// <summary>
    /// Метод, отвечающий за подпрыгивание игрока после убийства вражеского персонажа.
    /// </summary>
    public void JumpWhileHitting()
    {
        dinoRgdBd2D.velocity = new Vector2(dinoRgdBd2D.velocity.x, jumpForce - 1);
        isGrounded = false;
        animatorController.SetBool("isGrounded", isGrounded);
    }

    /// <summary>
    /// Метод, отвечающий за движение игрока вправо.
    /// </summary>
    public void MoveRight()
    {
        animatorController.SetFloat("speed", walkSpeed);
        direction = 1;
        animatorController.SetInteger("direction", direction);
        dinoRgdBd2D.position += Vector2.right * walkSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Метод, отвечающий за движение игрока влево.
    /// </summary>
    public void MoveLeft()
    {
        animatorController.SetFloat("speed", walkSpeed);
        direction = -1;
        animatorController.SetInteger("direction", direction);
        dinoRgdBd2D.position += Vector2.left * walkSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Метод, отвечающий за спокойное состояние объекта игрока.
    /// </summary>
    private void Idle()
    {
        animatorController.SetFloat("speed", 0);
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animatorController.SetBool("isGrounded", isGrounded);
        if (!isGrounded) return;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (Input.GetAxis("Horizontal") == 0 & isGrounded)
            Idle();
        if (lostLives >= 5)
            Die();
    }

    /// <summary>
    /// Метод, перезапускающий уровень при смерти персонажа.
    /// </summary>
    public void Die()
    {
        isDead = true;
    }

    /// <summary>
    /// Метод для поиска ближайшего к персонажу объекта заданного типа.
    /// </summary>
    /// <returns></returns>
    public GameObject FindClosest(string gameObjectTag)
    {
        GameObject[] objects;
        objects = GameObject.FindGameObjectsWithTag(gameObjectTag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (var gameObject in objects)
        {
            Vector3 diff = gameObject.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = gameObject;
                distance = curDistance;
            }
        }
        return closest;
    }
    /// <summary>
    /// Метод, отвечающий за получения урона персонажем.
    /// </summary>
    /// <param name="damage"></param>
    public void GetDamage(int damage)
    {
        lostLives += damage;
    }

    public IEnumerator UndamagedTimer()
    {
        wasHit = true;
        var hitState = direction == 1 ? "HitRight" : "HitLeft";
        animatorController.CrossFadeInFixedTime(hitState, 0.75f);
        yield return new WaitForSeconds(0.75f);
        var state = direction == 1 ? "IdleRight" : "IdleLeft";
        animatorController.Play(state);
        yield return new WaitForSeconds(1.5f);
        wasHit = false;
    }

    /// <summary>
    /// Метод взаимодействия с наносящими урон объектами в движении.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBody" && wasHit == false)
        {
            Vector2 directionVector = direction == -1 ? Vector2.right : Vector2.left;
            dinoRgdBd2D.position += directionVector * Time.deltaTime * 70;
            source.PlayOneShot(heroHitSound);
            GetDamage(2);
            StartCoroutine(UndamagedTimer());
        }

        if (other.tag == "Spikes" && wasHit == false)
        {
            Vector2 directionVector = direction == -1 ? Vector2.right : Vector2.left;
            dinoRgdBd2D.position += directionVector * Time.deltaTime * 70;
            source.PlayOneShot(heroHitSound);
            GetDamage(1);
            StartCoroutine(UndamagedTimer());
        }

        if (other.tag == "HealthBottle")
        {
            if (lostLives > 1)
            {
                lostLives -= 2;
                Destroy(FindClosest("HealthBottle"));
            }
            else
            if (lostLives > 0)
            {
                lostLives--;
                Destroy(FindClosest("HealthBottle"));
            }
            else Destroy(FindClosest("HealthBottle"));
        }

        if (other.tag == "EnemyHead")
        {
            JumpWhileHitting();
            Destroy(FindClosest("Enemy"));
            source.PlayOneShot(enemyHitSound);
        }

        if (other.tag == "LevelEnd")
        {
            EndLevelMenu.isEnded = true;
            EndLevelMenu.pauseMenuDisabled = true;
        }
    }

    /// <summary>
    /// Метод взаимодействия с наносящими урон объектами в спокойном положении.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "EnemyBody" && wasHit == false)
        {
            //  dinoRgdBd2D.velocity = directionVector * 5;
            Vector2 directionVector = direction == -1 ? Vector2.right : Vector2.left;
            dinoRgdBd2D.position += directionVector * Time.deltaTime * 80;
            source.PlayOneShot(heroHitSound);
            GetDamage(2);
            StartCoroutine(UndamagedTimer());
        }

        if (other.tag == "Spikes" && wasHit == false)
        {
            Vector2 directionVector = direction == -1 ? Vector2.right : Vector2.left;
            dinoRgdBd2D.position += directionVector * Time.deltaTime * 80;
            source.PlayOneShot(heroHitSound);
            GetDamage(1);
            StartCoroutine(UndamagedTimer());
        }
    }

    /// <summary>
    /// Метод, делающий объект игрока дочерним по отношению к движущейся платформе при запрыгивании на нее.
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(other.transform);
    }

    /// <summary>
    /// Метод, убирающий объект игрока из дочерних объектов движущейся платформы при ее покидании.
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(null);
    }
}

