using UnityEngine;
using TMPro;

public class UIManager2 : MonoBehaviour
{
    // Глобальная переменная для хранения состояния ключа
    public static bool KEY = false;

    // Ссылка на объект Canvas с текстом
    public Canvas canvas;

    // Ссылка на аудиофайл для проигрывания звука
    public AudioClip clickSound;

    // Скорость вращения объекта
    public float rotationSpeed = 30f;

    private AudioSource audioSource; // Источник аудио для проигрывания звука

    private void Start()
    {
        // Получаем компонент AudioSource, присоединенный к этому объекту
        audioSource = GetComponent<AudioSource>();

        // При старте игры скрываем Canvas с текстом
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Вращаем объект по оси Y с постоянной скоростью
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        // Проверяем, есть ли ключ
        if (!KEY)
        {
            // Устанавливаем значение ключа в true
            KEY = true;

            // Включаем Canvas с текстом
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
            }

            // Воспроизводим звук
            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }

            // Скрываем видимость объекта
            gameObject.SetActive(false);
        }
    }
}

