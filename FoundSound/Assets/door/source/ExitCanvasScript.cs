using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class ExitCanvasScript : MonoBehaviour
{
    public CanvasGroup exitCanvasGroup;
    public Button exitButton;
    public Transform playerTransform;
    public float detectionDistance = 7f;
    public float fadeDuration = 2f;
    public string nextSceneName;

    private bool isPlayerNear = false;

    void Start()
    {
        exitButton.onClick.AddListener(OnExitButtonClicked);

        if (playerTransform == null)
        {
            Debug.LogError("Player transform is not assigned!");
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if (distanceToPlayer <= detectionDistance && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeOutSceneAndLoadNext());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            StartCoroutine(FadeInExitCanvas());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            StartCoroutine(FadeOutExitCanvas());
        }
    }

    void OnExitButtonClicked()
    {
        StartCoroutine(FadeOutSceneAndLoadNext());
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator FadeInExitCanvas()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            exitCanvasGroup.alpha = alpha;
            timer += Time.deltaTime;
            yield return null;
        }

        exitCanvasGroup.alpha = 1f;
    }

    IEnumerator FadeOutExitCanvas()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            exitCanvasGroup.alpha = alpha;
            timer += Time.deltaTime;
            yield return null;
        }

        exitCanvasGroup.alpha = 0f;
    }

    IEnumerator FadeOutSceneAndLoadNext()
    {
        float timer = 0f;
        Color originalColor = Camera.main.backgroundColor;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            // Изменяем цвет заднего фона основной камеры
            Camera.main.backgroundColor = Color.Lerp(originalColor, Color.black, t);
            timer += Time.deltaTime;
            yield return null;
        }

        Camera.main.backgroundColor = Color.black;

        LoadNextScene();
    }
}


