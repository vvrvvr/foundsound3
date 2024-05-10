using UnityEngine;

public class DelayedStart : MonoBehaviour
{
    public DissolveManager dissolveManager;

    private void Start()
    {
        Invoke("StartDissolveManager", 727f); // 
    }

    private void StartDissolveManager()
    {
        if (dissolveManager != null)
        {
            dissolveManager.enabled = true;
        }
        else
        {
            Debug.LogWarning("DissolveManager is not assigned.");
        }
    }
}
