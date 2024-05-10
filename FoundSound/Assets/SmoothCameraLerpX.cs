using UnityEngine;

public class SmoothCameraLerpX : MonoBehaviour
{
    public float startX = 15f; // начальная позиция камеры по оси X
    public float targetX = 33f; // конечная позиция камеры по оси X
    public float duration = 40f; // продолжительность анимации в секундах

    private float elapsedTime = 0f;

    void Start()
    {
        // Сохраняем начальную позицию камеры
        startX = transform.position.x;
    }

    void Update()
    {
        // Увеличиваем время, прошедшее с начала анимации
        elapsedTime += Time.deltaTime;

        // Интерполируем позицию камеры по оси X от startX до targetX
        float newX = Mathf.Lerp(startX, targetX, elapsedTime / duration);

        // Применяем новую позицию камеры
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Если прошло больше или равно duration секунд, останавливаем анимацию
        if (elapsedTime >= duration)
        {
            enabled = false;
        }
    }
}
