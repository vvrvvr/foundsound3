using UnityEngine;

public class DelayedStartEnd3 : MonoBehaviour
{
    public DissolveManagerTerrain dissolveManagerTerrain;

    private void Start()
    {
        Invoke("StartDissolveManagerTerrain", 1150f); // 
    }

    private void StartDissolveManagerTerrain()
    {
        if (dissolveManagerTerrain != null)
        {
            dissolveManagerTerrain.enabled = true;
        }
        else
        {
            Debug.LogWarning("DissolveManagerTerrain is not assigned.");
        }
    }
}
