using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AgentBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform pointGroup;
    private Transform[] points;
    private int currentIndex;
    private Vector3 lastPosition;
    private int lastIndex;
    private bool isChasing = false;

    public AudioClip detectionClip; // 감지 효과음
    private AudioSource audioSource;
    private float patrolSpeed = 0.5f; // 순찰 속도 및 감지 속도

    public enum AGENT_STAGE
    {
        STAGE1, STAGE2, STAGE3
    }

    public AGENT_STAGE e_AGENT_STAGE;

    void Start()
    {
        if (pointGroup == null)
        {
            Debug.LogError("Point Group is not assigned!");
            return;
        }

        // AudioSource 컴포넌트 추가 및 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = detectionClip;
        audioSource.pitch = 0.5f; // 효과음 속도 0.5배

        // 부모 자신을 제외한 포인트 초기화
        List<Transform> pointList = new List<Transform>(pointGroup.GetComponentsInChildren<Transform>());
        pointList.Remove(pointGroup);
        points = pointList.ToArray();

        currentIndex = 0;
        SetNextDestination();

        agent.speed = patrolSpeed; // 게임 시작 시 속도 0.5로 설정
    }

    void Update()
    {
        if (!isChasing)
        {
            if (!agent.pathPending && agent.remainingDistance <= 0.23f)
            {
                currentIndex = (currentIndex + 1) % GetStagePointLimit();
                SetNextDestination();
            }
        }

        DetectPlayer();
    }

    private void SetNextDestination()
    {
        if (points.Length == 0)
        {
            Debug.LogWarning("No points assigned!");
            return;
        }

        agent.SetDestination(points[currentIndex].position);
    }

    private int GetStagePointLimit()
    {
        switch (e_AGENT_STAGE)
        {
            case AGENT_STAGE.STAGE1: return 3;
            case AGENT_STAGE.STAGE2: return 6;
            case AGENT_STAGE.STAGE3: return 10;
            default: return 3;
        }
    }

    public void OnSetStage(AGENT_STAGE stage)
    {
        e_AGENT_STAGE = stage;
        currentIndex = 0;
        SetNextDestination();
    }

    private void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Vector3 directionToPlayer = (hitCollider.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToPlayer) < 22.5f) // 45도 시야각의 반
                {
                    if (!isChasing)
                    {
                        isChasing = true;
                        lastPosition = agent.destination; // 마지막 목적지 저장
                        lastIndex = currentIndex; // 마지막 인덱스 저장
                        audioSource.Play(); // 효과음 재생
                    }
                    agent.SetDestination(hitCollider.transform.position);
                    return;
                }
            }
        }

        if (isChasing)
        {
            isChasing = false;
            agent.SetDestination(lastPosition); // 마지막 목적지로 복귀
            currentIndex = lastIndex; // 마지막 인덱스로 복귀
            audioSource.Stop(); // 효과음 정지
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
