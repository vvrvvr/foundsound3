using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
    public GameObject objectToDisable;
    public float disableDelay = 2f; // Время задержки перед отключением объекта

    private bool collisionOccurred = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InvisibleObject"))
        {
            collisionOccurred = true;
            Invoke("DisableObject", disableDelay);
        }
    }

    void DisableObject()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
    }
}
