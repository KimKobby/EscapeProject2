using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PsychopathNPC : MonoBehaviour
{
    public float detectionDistance = 3f; // NPC�� ĳ���͸� ������ �ִ� �Ÿ�
    public float detectionAngle = 90f;   // NPC�� ĳ���͸� ������ �þ� ����
    private float randomMoveRadius = 10f; // NPC�� �ʱ⿡ ������ ���� �̵� �ݰ�
    private float changeRadiusInterval = 1.5f; // �ݰ� ���� ����

    private NavMeshAgent agent;
    private GameObject player;
    private bool isGameOver = false;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player �±׸� ���� GameObject�� ã�� �� �����ϴ�.");
            enabled = false;
            return;
        }

        timer = changeRadiusInterval;
        SetRandomDestination();
    }

    void Update()
    {
        if (isGameOver)
            return;

        timer += Time.deltaTime;

        // ĳ���Ϳ��� �Ÿ��� ���� ���
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // ĳ���Ͱ� �þ� ���� �ְ� ���� ���� �Ÿ� ���� ���� ��� ����
        if (distanceToPlayer <= detectionDistance && angleToPlayer <= detectionAngle)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            // ���� �Ұ����� ��� ���� �̵�
            if (!agent.hasPath || agent.remainingDistance < 0.1f)
            {
                SetRandomDestination();
            }
        }

        // ���� �������� ���� �̵� �ݰ� ����
        if (timer >= changeRadiusInterval)
        {
            timer = 0;
            ChangeWanderRadius();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NPC�� ĳ���Ϳ��� �浹�Ͽ� ���� ����!");
            isGameOver = true;
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    // NPC�� �̵��� ���� ������ ����ϰ� NavMeshAgent�� �����ϴ� �Լ�
    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomMoveRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, randomMoveRadius, 1);

        agent.SetDestination(hit.position);
    }

    // ���� �̵� �ݰ��� �����ϴ� �Լ�
    void ChangeWanderRadius()
    {
        randomMoveRadius = Random.Range(5f, 15f); // �ݰ��� 5���� 15 ������ ���� ������ ����
        SetRandomDestination(); // ���ο� �ݰ����� �̵� ����
    }
}
