using System.Collections;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TypewriterEffect typewriterEffect;
    public TMP_Text textField;
    public string[] textArray;
    private int currentIndex = 0;

    void Start()
    {
        UpdateTextField();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentIndex < textArray.Length - 1)
            {
                currentIndex++;
                UpdateTextField();
            }
            else
            {
                textField.text = "...";
            }
        }
    }

    void UpdateTextField()
    {
        string currentText = textArray[currentIndex];
        typewriterEffect.InitializeTypewriter(new string[]{currentText});
    }
}

