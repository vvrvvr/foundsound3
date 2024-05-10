using UnityEngine;
using System.Collections;

public class TutorCameraSwitcher : MonoBehaviour
{
    public GameObject playerCamera;
    public Camera[] otherCameras;
    public GameObject objectToDisable;
    public float switchInterval = 2f;
    public float switchDelay = 1f;
    public float transitionDuration = 0.5f;

    private int currentCameraIndex = 0;
    private Coroutine switchCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (switchCoroutine == null)
            {
                switchCoroutine = StartCoroutine(SwitchCameras());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (switchCoroutine != null)
            {
                StopCoroutine(switchCoroutine);
                switchCoroutine = null;
                SwitchToPlayerCamera();
            }
        }
    }

    IEnumerator SwitchCameras()
    {
        for (int i = 0; i < otherCameras.Length; i++)
        {
            yield return new WaitForSeconds(switchDelay);

            // Disable player camera
            playerCamera.SetActive(false);

            // Fade out previous camera
            if (i > 0)
            {
                StartCoroutine(FadeCamera(otherCameras[i - 1], 1f, 0f, transitionDuration));
            }

            // Fade in next camera
            Camera nextCamera = otherCameras[i];
            nextCamera.gameObject.SetActive(true); // Включаем следующую камеру
            StartCoroutine(FadeCamera(nextCamera, 0f, 1f, transitionDuration));

            yield return new WaitForSeconds(switchInterval);
        }

        // Fade out last camera
        if (otherCameras.Length > 0)
        {
            StartCoroutine(FadeCamera(otherCameras[otherCameras.Length - 1], 1f, 0f, transitionDuration));
        }

        // Switch back to player camera
        SwitchToPlayerCamera();
    }

    void SwitchToPlayerCamera()
    {
        // Enable objectToDisable
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(true);
        }

        // Enable player camera
        playerCamera.SetActive(true);
    }

    IEnumerator FadeCamera(Camera camera, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = Mathf.Clamp01(elapsedTime / duration);
            Color color = camera.backgroundColor;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            camera.backgroundColor = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}

