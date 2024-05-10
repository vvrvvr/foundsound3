using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeStrengh = 1.0f;
    public float maxShakeDistance = 1.0f;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        Vector3 shakeOffset = (Vector3)(Random.insideUnitCircle * shakeStrengh);
        Vector3 newPosition = originalPosition + shakeOffset;

        // Ограничение на максимальное смещение
        float distance = Vector3.Distance(newPosition, originalPosition);
        if (distance > maxShakeDistance)
        {
            newPosition = originalPosition + (newPosition - originalPosition).normalized * maxShakeDistance;
        }

        transform.position = newPosition;

        if (shakeStrengh > 0)
            shakeStrengh = Mathf.Clamp(shakeStrengh - Time.deltaTime, 0, Mathf.Infinity);
    }
}
