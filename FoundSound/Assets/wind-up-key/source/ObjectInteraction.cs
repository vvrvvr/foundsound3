using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    // Глобальная переменная для хранения состояния ключа
    public static bool KEY = false;

    // Скорость вращения объекта
    public float rotationSpeed = 30f;

    private void Update()
    {
        // Вращаем объект по оси Y с постоянной скоростью
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        // Проверяем, есть ли ключ, если нет, то удаляем объект
        if (!KEY)
        {
            // Удаляем объект из сцены
            Destroy(gameObject);

            // Устанавливаем значение ключа в true
            KEY = true;
        }
    }
}
