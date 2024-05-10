using UnityEngine;

public class SmoothCameraLerpY : MonoBehaviour
{
    public float startY = 15f; // начальная позиция камеры по оси Y
    public float targetY = 33f; // конечная позиция камеры по оси Y
    public float duration = 40f; // продолжительность анимации в секундах

    private float elapsedTime = 0f;

    void Start()
    {
        // Сохраняем начальную позицию камеры
        startY = transform.position.y;
    }

    void Update()
    {
        // Увеличиваем время, прошедшее с начала анимации
        elapsedTime += Time.deltaTime;

        // Интерполируем позицию камеры по оси Y от startY до targetY
        float newY = Mathf.Lerp(startY, targetY, elapsedTime / duration);

        // Применяем новую позицию камеры
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Если прошло больше или равно duration секунд, останавливаем анимацию
        if (elapsedTime >= duration)
        {
            enabled = false;
        }
    }
}
