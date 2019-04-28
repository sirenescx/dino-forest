using UnityEngine;

/// <summary>
/// Класс для работы с перемещением камеры за персонажем.
/// </summary>
public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Вектор перемещения камеры.
    /// </summary>
    private Vector2 velocity;
    /// <summary>
    /// Минимальная позиция камеры по трем осям.
    /// </summary>
    public Vector3 minCameraPos;
    /// <summary>
    /// Максимальная позиция камеры по трем осям.
    /// </summary>
    public Vector3 maxCameraPos;
    /// <summary>
    /// Коэффициент сглаживания движения камеры.
    /// </summary>
    public float smoothTime;
    /// <summary>
    /// Персонаж.
    /// </summary>
    public Dino dino;

    void Start()
    {
        //Инициализация персонажа.
        dino = FindObjectOfType<Dino>();
    }
    void FixedUpdate()
    {
        //Определение позиции камеры относительно персонажа.
        float posX = Mathf.SmoothDamp(transform.position.x, dino.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, dino.transform.position.y, ref velocity.y, smoothTime);
        //Изменение позиции камеры.
        transform.position = new Vector2(posX, posY);
        //Границы перемещения камеры.
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                                         Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                                         Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
    }
}


