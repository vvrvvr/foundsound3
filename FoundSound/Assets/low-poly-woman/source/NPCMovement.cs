using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] movePoints; // Точки, между которыми будет передвигаться NPC
    public float playerDetectionRadius = 11f; // Радиус обнаружения игрока
    private int currentPoint = 0; // Индекс текущей точки
    private NavMeshAgent navMeshAgent;
    private bool isWaiting = false; // Флаг для определения, находится ли NPC в состоянии ожидания
    private float waitTimer = 0f; // Таймер для отслеживания времени ожидания
    private float waitTime = 7f; // Время ожидания в секундах
    private Transform playerTransform; // Позиция игрока
    private Quaternion originalRotation; // Изначальное вращение NPC

    void Start()
    {
        navMeshAgent = GetComponent
        <NavMeshAgent>(); // Получаем компонент NavMeshAgent у объекта NPC
        originalRotation = transform.rotation; // Сохраняем изначальное вращение NPC
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Находим позицию игрока
        MoveToNextPoint(); // Начинаем движение к первой точке
    }

    void Update()
    {
        // Если NPC находится в состоянии ожидания, уменьшаем таймер
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            // Если прошло достаточно времени, начинаем двигаться к следующей точке
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                MoveToNextPoint();
            }
            return;
        }

        // Проверяем, достиг ли NPC текущей точки
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= 10f)
        {
            // Если NPC достиг последней точки, начинаем двигаться к первой точке снова
            if (currentPoint == movePoints.Length - 1)
                currentPoint = 0;
            else
                currentPoint++; // Иначе двигаемся к следующей точке

            // Останавливаем NPC перед достижением точки и включаем состояние ожидания
            navMeshAgent.isStopped = true;
            isWaiting = true;
        }

        // Проверяем расстояние до игрока
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            // Останавливаем NPC и поворачиваем его в сторону игрока
            navMeshAgent.isStopped = true;
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0f, directionToPlayer.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            // Возвращаем изначальное вращение NPC
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, Time.deltaTime * 5f);
            navMeshAgent.isStopped = false;
        }
    }

    void MoveToNextPoint()
    {
        // Проверяем, есть ли точки, к которым нужно двигаться
        if (movePoints.Length == 0)
            return;

        // Устанавливаем позицию следующей точки, к которой нужно двигаться
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(movePoints[currentPoint].position);
    }
}
