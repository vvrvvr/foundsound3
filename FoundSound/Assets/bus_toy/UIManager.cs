using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMP_Text hintText; // Ссылка на текстовое поле на Canvas
    public Camera mainCamera; // Ссылка на главную камеру
    public CanvasGroup canvasGroup; // Ссылка на Canvas Group

    private void Start()
    {
        // При старте игры скрываем текст "нет ключа"
        hintText.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        // Проверяем значение ключа
        if (!ObjectInteraction.KEY)
        {
            // Если ключ отсутствует, показываем текст "нет ключа"
            hintText.gameObject.SetActive(true);
            hintText.text = "Нужен ключ.";
        }
        else
        {
            // Если ключ есть, запускаем корутину для изменения FOV и прозрачности
            StartCoroutine(TransitionToNextScene());
        }
    }

    IEnumerator TransitionToNextScene()
    {
        // Изменяем FOV камеры и прозрачность Canvas Group одновременно
        float startFOV = mainCamera.fieldOfView;
        float targetFOV = 173f;
        float startAlpha = 0f; // начальное значение прозрачности
        float targetAlpha = 1f; // конечное значение прозрачности
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Изменяем FOV камеры
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / duration);
            
            // Изменяем прозрачность Canvas Group
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            canvasGroup.alpha = currentAlpha;

            Debug.Log("Current FOV: " + mainCamera.fieldOfView); // Выводим текущее значение FOV в консоль
            Debug.Log("Current Alpha: " + currentAlpha); // Выводим текущее значение прозрачности в консоль

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем конечные значения FOV и прозрачности после завершения цикла
        mainCamera.fieldOfView = targetFOV;
        canvasGroup.alpha = targetAlpha;

        // Когда Canvas Group становится непрозрачным, осуществляем переход на следующую сцену
        SceneManager.LoadScene("Ending_2");
    }
}



