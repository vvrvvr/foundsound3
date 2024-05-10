using UnityEngine;

public class HideOnClick : MonoBehaviour
{
    public GameObject objectToHide;
    private bool isHidden = false;

    void Update()
    {
        // Проверяем, нажата ли левая кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            // Если объект для скрытия существует
            if (objectToHide != null)
            {
                // Если объект еще не скрыт, скрываем его
                if (!isHidden)
                {
                    objectToHide.SetActive(false);
                    isHidden = true;
                }
            }
            else
            {
                Debug.LogWarning("Object to hide is not assigned!");
            }
        }
    }
}