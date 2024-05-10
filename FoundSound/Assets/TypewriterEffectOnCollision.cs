using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffectOnCollision : MonoBehaviour
{
    public Canvas textCanvas;
    public TextMeshProUGUI textMeshPro;
    public string[] texts;
    public float typingSpeed = 0.1f;
    public float textDelay = 1f;

    private bool collisionOccurred = false;
    private Coroutine typingCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InvisibleObject"))
        {
            collisionOccurred = true;
            // Активируем Canvas
            textCanvas.gameObject.SetActive(true);
            // Начинаем вывод текста
            typingCoroutine = StartCoroutine(TypeText());
        }
    }

    IEnumerator TypeText()
    {
        // Проходим по всем текстам в массиве
        foreach (string text in texts)
        {
            // Сбрасываем текст перед началом печати
            textMeshPro.text = "";

            // Проходим по каждой букве в тексте и выводим поочередно
            foreach (char letter in text.ToCharArray())
            {
                textMeshPro.text += letter;
                yield return new WaitForSeconds(typingSpeed);
                yield return null; // Дожидаемся завершения вывода текущей буквы
            }

            // Ждем перед выводом следующего текста
            yield return new WaitForSeconds(textDelay);
        }
    }

    void OnDestroy()
    {
        // Останавливаем корутину, если она была запущена
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    }
}

