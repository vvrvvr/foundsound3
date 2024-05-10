using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Добавьте эту директиву

public class TypewriterEffect : MonoBehaviour
{
    public float delayBetweenLetters = 0.1f;
    public float shakeIntensity = 0.1f;

    private TextMeshProUGUI textMeshPro;
    private Coroutine typingCoroutine;
    private string[] textArray;
    private int currentIndex = 0;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void InitializeTypewriter(string[] texts)
    {
        textArray = texts;
        currentIndex = 0; // Сбрасываем индекс при инициализации
        StartTyping();
    }

    void StartTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        string currentText = "";
        string fullText = textArray[currentIndex];

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText += fullText[i];
            textMeshPro.text = currentText;

            // Apply shake effect
            Vector3 shakeOffset = new Vector3(Random.Range(-shakeIntensity, shakeIntensity), 
                                              Random.Range(-shakeIntensity, shakeIntensity), 
                                              0);
            textMeshPro.rectTransform.localPosition += shakeOffset;

            yield return new WaitForSeconds(delayBetweenLetters);
        }

        currentIndex++;

        // Check if there are more texts to type
        if (currentIndex < textArray.Length)
            StartTyping();
    }
}

