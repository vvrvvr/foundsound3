using UnityEngine;

public class ChangeTextureOnClick : MonoBehaviour
{
    public Texture newTexture; // Новая текстура, которую вы хотите применить
    private Texture originalTexture; // Исходная текстура объекта
    private Renderer objectRenderer; // Ссылка на компонент Renderer объекта
    private bool isTextureChanged = false; // Переменная состояния для отслеживания изменения текстуры

    void Start()
    {
        // Получаем компонент Renderer у объекта
        objectRenderer = GetComponent<Renderer>();
        // Сохраняем исходную текстуру объекта
        originalTexture = objectRenderer.material.mainTexture;
    }

    void Update()
    {
        // Проверяем, нажата ли кнопка мыши
        if (Input.GetMouseButtonDown(0)) // 0 означает левую кнопку мыши, 1 - правую, 2 - среднюю
        {
            // Если текстура объекта не менялась
            if (!isTextureChanged)
            {
                // Применяем новую текстуру к объекту
                objectRenderer.material.mainTexture = newTexture;
                isTextureChanged = true; // Устанавливаем флаг, что текстура была изменена
            }
            else
            {
                // Возвращаем исходную текстуру объекта
                objectRenderer.material.mainTexture = originalTexture;
                isTextureChanged = false; // Сбрасываем флаг
            }
        }
    }
}