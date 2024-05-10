using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeInAndLoadScene : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public float delayBeforeLoad = 7f;
    public float startAlphaChangeAfter = 1160f; // Время (в секундах), через которое начинается изменение альфа-значения
    public float alphaChangeDuration = 3f; // Длительность изменения альфа-значения

    private float timer = 0f;
    private bool isFading = false;
    private bool isLoadingScene = false;

    void Start()
    {
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        // Увеличиваем значение таймера
        timer += Time.deltaTime;

        // Если прошло указанное время, начинаем изменение альфа-значения
        if (timer > startAlphaChangeAfter && !isFading)
        {
            isFading = true;
            StartCoroutine(ChangeAlphaOverTime(canvasGroup.alpha, 1f, alphaChangeDuration));
        }

        // Когда изменение альфа-значения закончено и сцена не загружается
        if (isFading && !isLoadingScene)
        {
            // Запускаем загрузку сцены через указанное время
            Invoke("LoadMenuScene", delayBeforeLoad);
            isLoadingScene = true;
        }
    }

    IEnumerator ChangeAlphaOverTime(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            canvasGroup.alpha = newAlpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }

    void LoadMenuScene()
    {
        // Загрузка сцены по ее имени (menu)
        SceneManager.LoadScene("menu");
    }
}
