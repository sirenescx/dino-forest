using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

/// <summary>
/// Класс персонажа.
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
    internal int lostLives = 0;
    /// <summary>
    /// Контейнер для взаимодействия игрока 
    /// </summary>
    public Rigidbody2D dinoRgdBd2D;
    /// <summary>
    /// Контроллер игрока.
    /// </summary>
    Animator animatorController;
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
    static AudioSource source;
    /// <summary>
    /// Звук убийства врага.
    /// </summary>
    public AudioClip enemyHitSound;
    /// <summary>
    /// Звук получения урона.
    /// </summary>
    public AudioClip dinoHitSound;
    /// <summary>
    /// Жив персонаж или нет.
    /// </summary>
    public static bool isDead;
    /// <summary>
    /// Был ли получен урон.
    /// </summary>
    bool wasHit = false;
    /// <summary>
    /// Является ли уровень обучающим.
    /// </summary>
    public bool isTutorial;
    /// <summary>
    /// Сила подскока над головой вражеского персонажа при его убийстве игроком.
    /// </summary>
    public static float hittingJumpingForce;
    /// <summary>
    /// Границы зоны сражения с боссом.
    /// </summary>
    public GameObject[] borders;

    /// <summary>
    /// Получение значения громкости из файла PlayerPrefs.dat
    /// </summary>
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.volume = PlayerPrefs.GetFloat("VolumeValue");
    }

    /// <summary>
    /// Инициализация персонажа. Сброс количества монет и отображения меню конца уровня.
    /// </summary>
    void Start()
    {
        dinoRgdBd2D = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        isDead = false;
        CoinPick.coinCounter = 0;
        EndLevelMenu.isEnded = false;
        animatorController.SetInteger("direction", 1);
        source.volume = PlayerPrefs.GetFloat("VolumeValue");
    }

    /// <summary>
    /// Метод, отвечающий за прыжок игрока.
    /// </summary>
    internal void Jump()
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
    void JumpWhileHitting()
    {
        dinoRgdBd2D.velocity = new Vector2(dinoRgdBd2D.velocity.x, hittingJumpingForce);
        isGrounded = false;
        animatorController.SetBool("isGrounded", isGrounded);
    }

    /// <summary>
    /// Метод, отвечающий за движение игрока вправо.
    /// </summary>
    internal void MoveRight()
    {
        animatorController.SetFloat("speed", walkSpeed);
        direction = 1;
        animatorController.SetInteger("direction", direction);
        dinoRgdBd2D.position += Vector2.right * walkSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Метод, отвечающий за движение игрока влево.
    /// </summary>
    internal void MoveLeft()
    {
        animatorController.SetFloat("speed", walkSpeed);
        direction = -1;
        animatorController.SetInteger("direction", direction);
        dinoRgdBd2D.position += Vector2.left * walkSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Метод, отвечающий за спокойное состояние объекта игрока.
    /// </summary>
    void Idle()
    {
        animatorController.SetFloat("speed", 0);
    }

    /// <summary>
    /// Проверка нахождения персонажа на твердой поверхности.
    /// </summary>
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animatorController.SetBool("isGrounded", isGrounded);
        if (!isGrounded)
            return;
    }

    /// <summary>
    /// Изменение громкости звука. Отслеживание состояния персонажа.
    /// </summary>
    void Update()
    {
        try
        {
            source.volume = PlayerPrefs.GetFloat("VolumeValue");
        }
        catch (NullReferenceException) { }

        if (Input.GetAxis("Horizontal") == 0 & isGrounded)
            Idle();

        if (lostLives >= 5)
            Die();
    }

    /// <summary>
    /// Метод, перезапускающий уровень при смерти персонажа.
    /// </summary>
    internal void Die()
    {
        isDead = true;
    }

    /// <summary>
    /// Метод для поиска ближайшего к персонажу объекта заданного типа.
    /// </summary>
    /// <returns></returns>
    GameObject FindClosest(string gameObjectTag)
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
    void GetDamage(int damage, int inertia)
    {
        lostLives += damage;
        Vector2 directionVector = direction == -1 ? Vector2.right : Vector2.left;
        dinoRgdBd2D.position += directionVector * Time.deltaTime * inertia;
        try
        {
            source.PlayOneShot(dinoHitSound);
        }
        catch (NullReferenceException) { }
    }

    /// <summary>
    /// Метод для запуска таймера после получения персонажем урона, во время которого персонаж больше не может получать урон.
    /// </summary>
    /// <returns></returns>
    IEnumerator UndamagedTimer()
    {
        wasHit = true;
        var hitState = direction == 1 ? "HitRight" : "HitLeft";
        animatorController.CrossFadeInFixedTime(hitState, 0.75f);
        yield return new WaitForSeconds(0.75f);
        var state = direction == 1 ? "IdleRight" : "IdleLeft";
        animatorController.Play(state);
        yield return new WaitForSeconds(1f);
        wasHit = false;
    }

    /// <summary>
    /// Метод для обработки нанесения пользователем удара вражескому персонажу.
    /// </summary>
    void HitEnemy()
    {
        JumpWhileHitting();
        Destroy(FindClosest("Enemy"));
        source.PlayOneShot(enemyHitSound);
    }

    /// <summary>
    /// Активация границ зоны сражения с боссом.
    /// </summary>
    void ActivateBorders()
    {
        foreach (var border in borders)
            border.gameObject.SetActive(true);
    }

    /// <summary>
    /// Снятие границ зоны сражения с боссом.
    /// </summary>
    void DisableBorders()
    {
        foreach (var border in borders)
            Destroy(border.gameObject);
    }

    /// <summary>
    /// Метод для обработки нанесения пользователем удара боссу.
    /// </summary>
    void HitBoss()
    {
        if (Boss.lives <= 0)
        {
            StartCoroutine(Boss.DeathTime());
            Destroy(FindObjectOfType<Boss>().gameObject);
            DisableBorders();
        }
        else
        {
            JumpWhileHitting();
            Boss.lives--;
            StartCoroutine(Boss.BossUndamagedTimer());
            FindObjectOfType<AudioSource>().PlayOneShot(enemyHitSound);
        }
    }

    /// <summary>
    /// Метод взаимодействия с наносящими урон объектами в движении.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "EnemyBody" | other.tag == "BossBody") && wasHit == false)
        {
            GetDamage(2, 70);
            StartCoroutine(UndamagedTimer());
        }

        if (other.tag == "Spikes" && wasHit == false)
        {
            GetDamage(1, 70);
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
        }

        if (other.tag == "EnemyHead")
            HitEnemy();

        if (other.tag == "BossHead")
            HitBoss();

        if (other.tag == "BossFlag")
            ActivateBorders();

        if (other.tag == "LevelEnd")
        {
            EndLevelMenu.isEnded = true;
            EndLevelMenu.pauseMenuDisabled = true;
            if (!isTutorial)
                PlayerPrefs.SetInt("openedLevels", int.Parse(SceneManager.GetActiveScene().name.Substring(5, 1)));
        }
    }

    /// <summary>
    /// Метод взаимодействия с наносящими урон объектами в спокойном положении.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.tag == "EnemyBody" | other.tag == "BossBody") && wasHit == false)
        {
            GetDamage(2, 80);
            StartCoroutine(UndamagedTimer());
        }

        if (other.tag == "Spikes" && wasHit == false)
        {
            GetDamage(1, 80);
            StartCoroutine(UndamagedTimer());
        }
    }

    /// <summary>
    /// Метод, делающий объект игрока дочерним по отношению к движущейся платформе при запрыгивании на нее.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(other.transform);
    }

    /// <summary>
    /// Метод для обработки поведения персонажа в статическом состоянии на движущейся платформе.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionStay2D(Collision2D other)
    {
        Vector2 inertia = new Vector2(0, -MovingPlatform.speed);
        if (other.gameObject.CompareTag("MovingPlatform"))
            dinoRgdBd2D.velocity = inertia;
    }

    /// <summary>
    /// Метод, убирающий объект игрока из дочерних объектов движущейся платформы при ее покидании.
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
            transform.SetParent(null);
    }
}