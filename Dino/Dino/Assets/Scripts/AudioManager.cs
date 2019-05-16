using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс для работы со звуком в основном меню.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Ссылка на объект типа AudioManager.
    /// </summary>
    static AudioManager instance;
    /// <summary>
    /// Источник звука.
    /// </summary>
    AudioSource source;

    /// <summary>
    /// Свойство доступа к объекту типа AudioManager для проверки на повторение звуковой темы.
    /// </summary>
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    /// <summary>
    /// Метод, загружающий тему и устанавливающий громкость ее воспроизведения.
    /// </summary>
    void Awake()
    {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (this != instance)
                    Destroy(gameObject);
            }

        source = GetComponent<AudioSource>();
        source.volume = PlayerPrefs.GetFloat("VolumeValue");
    }

    /// <summary>
    /// Обновление громкости звука и проверка перехода на сцену уровня.
    /// </summary>
    private void Update()
    {
        source.volume = PlayerPrefs.GetFloat("VolumeValue");

        if (SceneManager.GetActiveScene().name.Substring(0, 5) == "Scene")
        {
            Destroy(gameObject);
        }
    }
}