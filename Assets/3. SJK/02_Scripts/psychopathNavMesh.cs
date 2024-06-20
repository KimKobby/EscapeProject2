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

    public AudioClip detectionClip; // ���� ȿ����
    private AudioSource audioSource;
    private float patrolSpeed = 0.5f; // ���� �ӵ� �� ���� �ӵ�

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

        // AudioSource ������Ʈ �߰� �� �ʱ�ȭ
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = detectionClip;
        audioSource.pitch = 0.5f; // ȿ���� �ӵ� 0.5��

        // �θ� �ڽ��� ������ ����Ʈ �ʱ�ȭ
        List<Transform> pointList = new List<Transform>(pointGroup.GetComponentsInChildren<Transform>());
        pointList.Remove(pointGroup);
        points = pointList.ToArray();

        currentIndex = 0;
        SetNextDestination();

        agent.speed = patrolSpeed; // ���� ���� �� �ӵ� 0.5�� ����
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
                if (Vector3.Angle(transform.forward, directionToPlayer) < 22.5f) // 45�� �þ߰��� ��
                {
                    if (!isChasing)
                    {
                        isChasing = true;
                        lastPosition = agent.destination; // ������ ������ ����
                        lastIndex = currentIndex; // ������ �ε��� ����
                        audioSource.Play(); // ȿ���� ���
                    }
                    agent.SetDestination(hitCollider.transform.position);
                    return;
                }
            }
        }

        if (isChasing)
        {
            isChasing = false;
            agent.SetDestination(lastPosition); // ������ �������� ����
            currentIndex = lastIndex; // ������ �ε����� ����
            audioSource.Stop(); // ȿ���� ����
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
