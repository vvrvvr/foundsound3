using UnityEngine;
using TMPro;

public class HoverAndClickTMP : MonoBehaviour
{
    public TMP_Text hoverText;
    public string initialText;
    public string alternateText;
    public TMP_Text objectCountText; // UI элемент для отображения ObjectCount
    private static int objectCount = 0; // Статическая переменная ObjectCount

    private bool isClicked = false;
    private bool isHovered = false;

    private void Start()
    {
        hoverText.text = initialText;
        hoverText.enabled = false; // Изначально текст выключен
    }

    private void Update()
    {
        // Если текст активен и не было нажатия, то возвращаем его к начальному тексту
        if (isHovered && !isClicked)
        {
            hoverText.text = initialText;
        }
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        hoverText.enabled = true; // Включаем текст при наведении курсора на объект

        // Если было нажатие, меняем текст
        if (isClicked)
        {
            hoverText.text = alternateText;
        }
    }

    private void OnMouseExit()
    {
        isHovered = false;
        hoverText.enabled = false; // Выключаем текст при выходе курсора из области объекта
    }

    private void OnMouseDown()
    {
        if (!isClicked)
        {
            hoverText.enabled = false; // Скрываем текст при клике
            Invoke("ChangeToAlternateText", 5f); // Задержка перед сменой текста
            isClicked = true;

            // Включаем компонент AudioSource на объекте, если он есть
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.enabled = true;
            }

            // Увеличиваем ObjectCount при нажатии на любой объект
            objectCount++;
            objectCountText.text = "Записей: " + objectCount.ToString(); // Обновляем текст UI элемента
        }
    }

    private void ChangeToAlternateText()
    {
        if (isClicked)
        {
            hoverText.enabled = true; // Показываем текст после задержки
            hoverText.text = alternateText; // Меняем текст по клику
        }
    }
}
