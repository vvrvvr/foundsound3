using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float delayBeforeStart = 224f; // Задержка перед началом движения объекта
    public float startPositionX = 183f; // Начальная позиция по оси X
    public float endPositionX = 204f; // Конечная позиция по оси X
    public float movementDuration = 10f; // Продолжительность движения

    private float elapsedTime = 0f;
    private bool isMoving = false;

    private void Start()
    {
        Invoke("StartMovement", delayBeforeStart);
    }

    private void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / movementDuration);
            float newX = Mathf.Lerp(startPositionX, endPositionX, t);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }

    private void StartMovement()
    {
        isMoving = true;
    }
}
