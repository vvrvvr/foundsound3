using UnityEngine;
using TMPro;

public class ObjectCounter : MonoBehaviour
{
    public TMP_Text counterText; // Ссылка на текстовое поле TextMeshPro
    private int counter = 0; // Счётчик объектов
    private bool counted = false; // Флаг, показывающий, был ли этот объект уже учтен

    void Start()
    {
        counterText.text = "Count: " + counter.ToString(); // Установка начального значения счетчика
    }

    // Функция, вызываемая при клике на объекте
    public void OnObjectClicked()
    {
        // Проверяем, был ли этот объект уже учтен
        if (!counted)
        {
            // Проверяем, что объект имеет тег "HighlightGroup"
            if (gameObject.CompareTag("HighlightGroup"))
            {
                // Увеличиваем счётчик и обновляем текстовое поле
                counter++;
                counterText.text = "Count: " + counter.ToString();

                // Устанавливаем флаг counted в true, чтобы объект больше не учитывался
                counted = true;
            }
        }
    }
}
