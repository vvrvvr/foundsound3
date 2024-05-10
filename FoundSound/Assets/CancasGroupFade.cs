using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasFadeAndTransition : MonoBehaviour
{
    public float delayBeforeStart = 244f; // Задержка перед началом изменения прозрачности
    public float fadeDuration = 5f; // Продолжительность изменения прозрачности
    public string nextSceneName = "Menu"; // Название следующей сцены для перехода

    private CanvasGroup canvasGroup;
    private float elapsedTime = 0f;
    private bool isFading = false;
    private bool isAudioFading = false;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasFadeAndTransition script requires a CanvasGroup component on the same GameObject.");
            enabled = false;
            return;
        }

        canvasGroup.alpha = 0f; // Начинаем с нулевой прозрачности
        Invoke("StartFade", delayBeforeStart);
    }

    private void Update()
    {
        if (isFading)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            if (t >= 1f)
            {
                isFading = false;
                Invoke("LoadNextScene", 10f); // Загрузить следующую сцену через 10 секунд после достижения максимальной прозрачности
                if (!isAudioFading)
                {
                    isAudioFading = true;
                    StartCoroutine(FadeOutAudio());
                }
            }
        }
    }

    private void StartFade()
    {
        isFading = true;
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private System.Collections.IEnumerator FadeOutAudio()
    {
        AudioListener listener = FindObjectOfType<AudioListener>();
        if (listener == null)
        {
            Debug.LogError("No AudioListener found in the scene.");
            yield break;
        }

        float startVolume = AudioListener.volume;
        float currentTime = 0;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(startVolume, 0f, currentTime / fadeDuration);
            yield return null;
        }

        AudioListener.volume = 0f;
    }
}
