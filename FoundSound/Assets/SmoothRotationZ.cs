using UnityEngine;

public class SmoothRotationZ : MonoBehaviour
{
    public float startRotationZ = -18f; // начальный угол поворота объекта по оси Z
    public float targetRotationZ = 13f; // конечный угол поворота объекта по оси Z
    public float duration = 25f; // продолжительность анимации в секундах

    private float elapsedTime = 0f;

    void Start()
    {
        // Сохраняем начальный угол поворота объекта
        startRotationZ = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // Увеличиваем время, прошедшее с начала анимации
        elapsedTime += Time.deltaTime;

        // Интерполируем угол поворота объекта по оси Z от startRotationZ до targetRotationZ
        float newRotationZ = Mathf.Lerp(startRotationZ, targetRotationZ, elapsedTime / duration);

        // Создаем новый вектор поворота
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, newRotationZ);

        // Применяем новый поворот объекта
        transform.rotation = newRotation;

        // Если прошло больше или равно duration секунд, останавливаем анимацию
        if (elapsedTime >= duration)
        {
            enabled = false;
        }
    }
}
