using UnityEngine;
using TMPro;

public class TextDisplayTMP : MonoBehaviour
{
    public TMP_Text displayText; // ссылка на текстовый элемент TextMeshPro
    public string[] phrases; // массив фраз для отображения

    private bool isTextVisible = false; // флаг, указывающий, отображается ли текст в данный момент

    private void Start()
    {
        HideText(); // скрываем текст при запуске сцены
    }

    private void Update()
    {
        // Проверяем расстояние между объектом и игроком
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > 4f)
        {
            // Если расстояние больше 4, скрываем текст
            HideText();
        }
    }

    private void OnMouseDown()
    {
        ToggleText(); // переключаем отображение текста
    }

    void ToggleText()
    {
        if (isTextVisible) // если текст уже отображается
        {
            HideText(); // скрываем текст
        }
        else
        {
            ShowRandomText(); // иначе показываем текст
        }
    }

    void ShowRandomText()
    {
        // Генерируем случайный индекс от 0 до (длины массива - 1)
        int randomIndex = Random.Range(0, phrases.Length);
        
        // Устанавливаем текст для отображения
        displayText.text = phrases[randomIndex];
        // Активируем текст
        displayText.gameObject.SetActive(true);

        isTextVisible = true; // устанавливаем флаг, что текст отображается
    }

    void HideText()
    {
        displayText.gameObject.SetActive(false); // скрываем текст
        isTextVisible = false; // сбрасываем флаг
    }
}

