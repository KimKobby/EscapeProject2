using UnityEngine;
using UnityEngine.AI;

public class PsychoRandomMovement : MonoBehaviour
{
    public float wanderRadius = 10f; // NPC�� �̵��� �ݰ�
    public float chaseDistance = 10f; // �÷��̾ �����ϱ� �����ϴ� �Ÿ�
    private NavMeshAgent navMeshAgent;
    public Transform player; // �÷��̾��� Transform

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // "Player" �±׸� ���� ��ü�� ã�� �÷��̾�� ����
        Wander(); // ������ �� ���� �̵� ����
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �÷��̾ ���� �Ÿ� �ȿ� ���� �� ���� ����
        if (distanceToPlayer <= chaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            // NavMeshAgent�� ��ǥ ������ �����ϸ� �ٽ� ���� �̵�
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                Wander();
            }

            // NPC�� �̵��ϴ� �������� ȸ��
            RotateTowardsMovementDirection();

            // NavMesh �̵��� ������ ��� �ٷ� ��ġ ����
            if (navMeshAgent.isPathStale || !navMeshAgent.hasPath || navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                Wander();
            }
        }
    }

    // �÷��̾ �����ϴ� �Լ�
    void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    // NPC�� �̵��ϴ� �������� ȸ���ϴ� �Լ�
    void RotateTowardsMovementDirection()
    {
        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        }
    }

    void Wander()
    {
        int attempts = 0; // �õ� Ƚ�� �ʱ�ȭ
        while (attempts < 10) // �ִ� �õ� Ƚ��
        {
            // NPC �ֺ��� ������ ��ġ ����
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
            {
                // ������ ���� ��ġ�� �̵� ���
                navMeshAgent.SetDestination(hit.position);
                break; // ��ȿ�� ��ġ�� ã�����Ƿ� ���� ����
            }
            attempts++; // �õ� Ƚ�� ����
        }
    }
}
