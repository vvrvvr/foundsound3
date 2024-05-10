using UnityEngine;

public class SmoothCameraLerpZ : MonoBehaviour
{
    public float startZ = 381f; // начальная позиция камеры по оси Z
    public float targetZ = 533f; // конечная позиция камеры по оси Z
    public float duration = 40f; // продолжительность анимации в секундах

    private float elapsedTime = 0f;

 void Start()
    {
        // Сохраняем начальную позицию камеры
        startZ = transform.position.z;
    }



    void Update()
    {
        // Увеличиваем время прошедшее с начала анимации
        elapsedTime += Time.deltaTime;

        // Интерполируем позицию камеры по оси Z от startZ до targetZ
        float newZ = Mathf.Lerp(startZ, targetZ, elapsedTime / duration);

        // Применяем новую позицию камеры
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);

        // Если прошло больше или равно duration секунд, останавливаем анимацию
        if (elapsedTime >= duration)
        {
            enabled = false;
        }
    }
}
