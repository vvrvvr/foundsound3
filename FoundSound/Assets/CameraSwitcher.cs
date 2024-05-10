using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject[] cameraObjects; // массив объектов камер
    public float[] durations; // массив длительностей для каждой камеры
    private int currentCameraIndex = 0; // индекс текущей камеры
    private float switchTime; // время переключения

    void Start()
    {
        // Включаем первую камеру
        SetCameraEnabled(currentCameraIndex, true);
        // Устанавливаем время первого переключения
        switchTime = Time.time + durations[currentCameraIndex];
    }

    void Update()
    {
        // Проверяем, прошло ли время для переключения камер
        if (Time.time >= switchTime)
        {
            // Переключаемся на следующую камеру
            currentCameraIndex = (currentCameraIndex + 1) % cameraObjects.Length;
            SetCameraEnabled(currentCameraIndex, true);
            // Устанавливаем время следующего переключения
            switchTime = Time.time + durations[currentCameraIndex];
        }
    }

    // Включает или отключает указанную камеру
    void SetCameraEnabled(int index, bool enabled)
    {
        cameraObjects[index].SetActive(enabled);
    }
}
