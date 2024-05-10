using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationController : MonoBehaviour
{
    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    // Пороговое значение для определения движения
    public float movementThreshold = 0.1f;

    void Start()
    {
        // Получаем компоненты Animator и NavMeshAgent у объекта
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();// Устанавливаем анимацию по умолчанию
        animator.SetBool("IsWalking", false);
}

void Update()
{
    // Получаем скорость NavMeshAgent
    float speed = navMeshAgent.velocity.magnitude;

    // Проверяем, движется ли NPC
    if (speed > movementThreshold)
    {
        animator.SetBool("IsWalking", true);
    }
    else
    {
        animator.SetBool("IsWalking", false);
    }
}
}
