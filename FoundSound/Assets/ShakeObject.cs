using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    // Параметры дрожания
    public float shakeSpeed = 1.0f; // Скорость дрожания
    public float shakeAmount = 1.0f; // Амплитуда дрожания

    private Vector3 originalPosition; // Исходная позиция объекта
    private Transform parentTransform; // Трансформ основного объекта

    void Start()
    {
        // Сохраняем исходную позицию объекта относительно основного
        parentTransform = transform.parent;
        originalPosition = parentTransform.InverseTransformPoint(transform.position);
    }

    void Update()
    {
        // Вычисляем смещение по осям вращения
        float offsetX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0) * 2 - 1;
        float offsetY = Mathf.PerlinNoise(0, Time.time * shakeSpeed) * 2 - 1;
        float offsetZ = Mathf.PerlinNoise(Time.time * shakeSpeed, Time.time * shakeSpeed) * 2 - 1;

        // Применяем смещение к исходной позиции объекта с учетом амплитуды
        Vector3 shakeOffset = new Vector3(offsetX, offsetY, offsetZ) * shakeAmount;
        
        // Корректируем позицию объекта в соответствии с позицией основного объекта
        Vector3 newPosition = parentTransform.TransformPoint(originalPosition + shakeOffset);
        
        // Применяем новую позицию к объекту
        transform.position = newPosition;
    }
}
