using UnityEngine;
using UnityEngine.AI;

public class PsychoRandomMovement : MonoBehaviour
{
    public float wanderRadius = 10f; // NPC가 이동할 반경
    public float chaseDistance = 10f; // 플레이어를 추적하기 시작하는 거리
    private NavMeshAgent navMeshAgent;
    public Transform player; // 플레이어의 Transform

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // "Player" 태그를 가진 객체를 찾아 플레이어로 설정
        Wander(); // 시작할 때 랜덤 이동 시작
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 플레이어가 일정 거리 안에 있을 때 추적 시작
        if (distanceToPlayer <= chaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            // NavMeshAgent가 목표 지점에 도착하면 다시 랜덤 이동
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                Wander();
            }

            // NPC가 이동하는 방향으로 회전
            RotateTowardsMovementDirection();

            // NavMesh 이동에 실패한 경우 바로 위치 변경
            if (navMeshAgent.isPathStale || !navMeshAgent.hasPath || navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                Wander();
            }
        }
    }

    // 플레이어를 추적하는 함수
    void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    // NPC가 이동하는 방향으로 회전하는 함수
    void RotateTowardsMovementDirection()
    {
        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
        }
    }

    void Wander()
    {
        int attempts = 0; // 시도 횟수 초기화
        while (attempts < 10) // 최대 시도 횟수
        {
            // NPC 주변의 랜덤한 위치 생성
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
            {
                // 생성된 랜덤 위치로 이동 명령
                navMeshAgent.SetDestination(hit.position);
                break; // 유효한 위치를 찾았으므로 루프 종료
            }
            attempts++; // 시도 횟수 증가
        }
    }
}
