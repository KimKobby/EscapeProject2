using UnityEngine;
using UnityEngine.AI;

public class PsychoRandomMovement : MonoBehaviour
{
    public float wanderRadius = 10f; // NPC�� �̵��� �ݰ�
    public float chaseDistance = 10f; // �÷��̾ �����ϱ� �����ϴ� �Ÿ�
    private NavMeshAgent navMeshAgent;
    public Transform player; // �÷��̾��� Transform
    public AudioSource footstepAudioSource; // �߰��� �Ҹ� ����� �ҽ�
    public float maxVolumeDistance = 3f; // �Ҹ��� �ִ��� �Ÿ�

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform; // "Player" �±׸� ���� ��ü�� ã�� �÷��̾�� ����
            }
            else
            {
                Debug.LogError("Player with tag 'Player' not found!");
            }
        }
        if (footstepAudioSource == null)
        {
            footstepAudioSource = GetComponent<AudioSource>(); // ����� �ҽ� ��������
        }

        // �߰��� �Ҹ� �ӵ� ���� (0.5�� ���߱�)
        footstepAudioSource.pitch = 0.5f;
        footstepAudioSource.volume = 1.0f; // ���� �ʱ�ȭ
        footstepAudioSource.Play(); // ����� �ҽ� ���

        Wander(); // ������ �� ���� �̵� ����
    }

    void Update()
    {
        if (player == null) return;

        // y ���� ������ �Ÿ� ���
        Vector3 npcPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPosition = new Vector3(player.position.x, 0, player.position.z);
        float distanceToPlayer = Vector3.Distance(npcPosition, playerPosition);

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

        // �߰��� �Ҹ� ũ�� ����
        AdjustFootstepVolume(distanceToPlayer);
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

    // �߰��� �Ҹ� ũ�� ���� �Լ�
    void AdjustFootstepVolume(float distanceToPlayer)
    {
        // �Ÿ� ���� ��� (0�� �÷��̾�� ���� �����, 1�� maxVolumeDistance �̻� �־���)
        float distanceRatio = Mathf.Clamp01(distanceToPlayer / maxVolumeDistance);
        // �Ҹ� ũ�� ���� (�������� �Ҹ��� Ŀ��)
        footstepAudioSource.volume = 1 - distanceRatio;
    }
}
