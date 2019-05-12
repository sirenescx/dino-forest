using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    /// <summary>
    /// Слайдер для изменения громкости.
    /// </summary>
    public Slider volumeSlider;
    /// <summary>
    /// Громкость.
    /// </summary>
    public static float volume;

    private void Awake()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeValue");
    }
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeValue");
    }

    /// <summary>
    /// Метод изменения громкости звуков.
    /// </summary>
    public void VolumeChanged()
    {
        volume = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeSlider.value);
    }

    /// <summary>
    /// Метод перехода к основному игровому меню.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
