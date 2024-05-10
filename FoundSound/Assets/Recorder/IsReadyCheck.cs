using UnityEngine;

public class ChangeIsReadyOnCollision : MonoBehaviour
{
    public static bool IsReady = false;
    public GameObject objectToShow; // Ссылка на объект, который должен появиться

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsReady = true;
            ShowObjectIfReady();
        }
    }

    // Проверяем, нужно ли показать объект
    private void ShowObjectIfReady()
    {
        if (IsReady = true)
        {
            objectToShow.SetActive(true);
        }
    }
}