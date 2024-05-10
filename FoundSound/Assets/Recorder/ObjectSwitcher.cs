using UnityEngine;
using System.Collections;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject firstObj;
    public GameObject secondObj;
    private bool firstObj_burned = false;
    private bool secondObj_burned = false;
    private bool secondObj_ready = false;
    private bool secondObj_activated = false; // Переменная для отслеживания активации secondObj

    void Start()
    {
        // Убеждаемся, что secondObj изначально выключен
        secondObj.SetActive(false);
    }

    void Update()
    {
        // Проверяем, нажата ли кнопка F
        if (Input.GetKeyDown(KeyCode.F))
        {
            firstObj.SetActive(false);
            firstObj_burned = true;
            secondObj_ready = true;

            // Активируем secondObj только если он еще не был активирован ранее
            if (!secondObj_activated)
            {
                secondObj_activated = true;
                StartCoroutine(ActivateSecondObjectAfterDelay());
            }
        }
        
        // Проверяем, была ли нажата кнопка мыши, чтобы выключить объект secondObj
        else if (Input.GetMouseButtonDown(0))
        {
            secondObj.SetActive(false);
            secondObj_burned = true;
        }
    }

    IEnumerator ActivateSecondObjectAfterDelay()
    {
        // Ждем 4 секунды перед активацией secondObj
        yield return new WaitForSeconds(4f);

        secondObj.SetActive(true);
        secondObj_ready = false; // Устанавливаем обратно в false, чтобы объект secondObj мог быть активирован снова
    }
}


