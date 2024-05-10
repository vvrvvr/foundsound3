using System.Collections;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera playerCamera; // Ссылка на камеру игрока
    public Camera npcCamera; // Ссылка на камеру NPC
    public GameObject additionalObject; // Дополнительный объект, который нужно включать/выключать
    public KeyCode switchKey = KeyCode.E; // Кнопка для переключения камеры
    public float interactionDistance = 10f; // Расстояние для взаимодействия с NPC
    public Canvas npcCanvas; // Ссылка на Canvas, который должен включаться с камерой NPC
    public float npcFOV = 70f; // Новое значение Field of View для камеры NPC
    public float fovChangeDuration = 1.5f; // Продолжительность изменения FOV

    private Transform playerTransform; // Ссылка на трансформ игрока
    private bool isNPCCameraActive = false; // Флаг, определяющий активность камеры NPC

    void Start()
    {
        playerTransform = playerCamera.transform; // Получаем трансформ игрока
    }

    void Update()
    {
        // Проверяем, была ли нажата кнопка E
        if (Input.GetKeyDown(switchKey))
        {
            // Если камера NPC активна, деактивируем ее и включаем камеру игрока
            if (isNPCCameraActive)
            {
                npcCamera.gameObject.SetActive(false); // Отключаем камеру NPC
                playerCamera.gameObject.SetActive(true); // Включаем камеру игрока
                npcCanvas.gameObject.SetActive(false); // Отключаем Canvas NPC
                additionalObject.SetActive(true); // Включаем дополнительный объект
                isNPCCameraActive = false; // Устанавливаем флаг активности камеры NPC в false
                npcCamera.fieldOfView = 163f; // Возвращаем FOV камеры NPC к 163
            }
            // Если камера NPC не активна и ближайший объект NPC найден и он находится на расстоянии меньше interactionDistance
            else
            {
                // Находим ближайший объект NPC
                GameObject nearestNPC = FindNearestNPC();

                // Если ближайший объект NPC найден и он находится на расстоянии меньше interactionDistance
                if (nearestNPC != null && Vector3.Distance(nearestNPC.transform.position, playerTransform.position) <= interactionDistance)
                {
                    npcCamera.gameObject.SetActive(true); // Включаем камеру NPC
                    playerCamera.gameObject.SetActive(false); // Отключаем камеру игрока
                    npcCanvas.gameObject.SetActive(true); // Включаем Canvas NPC
                    additionalObject.SetActive(false); // Выключаем дополнительный объект
                    isNPCCameraActive = true; // Устанавливаем флаг активности камеры NPC в true
                    StartCoroutine(ChangeNPCCameraFOV()); // Запускаем корутину для изменения FOV камеры NPC
                }
            }
        }
    }

    // Метод для поиска ближайшего объекта NPC
    private GameObject FindNearestNPC()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC"); // Находим все объекты с тегом "NPC"

        GameObject nearestNPC = null;
        float minDistance = Mathf.Infinity;

        // Проходим по всем найденным объектам и находим ближайший
        foreach (GameObject npc in npcs)
        {
            float distance = Vector3.Distance(npc.transform.position, playerTransform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNPC = npc;
            }
        }

        return nearestNPC;
    }

    // Корутина для плавного изменения FOV камеры NPC
    private IEnumerator ChangeNPCCameraFOV()
    {
        float elapsedTime = 0f;
        float startFOV = npcCamera.fieldOfView;

        // Плавно изменяем FOV камеры NPC к новому значению
        while (elapsedTime < fovChangeDuration)
        {
            float t = elapsedTime / fovChangeDuration;
            npcCamera.fieldOfView = Mathf.Lerp(startFOV, npcFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}


