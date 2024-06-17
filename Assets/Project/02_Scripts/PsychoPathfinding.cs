using UnityEngine;
using UnityEngine.AI;

public class PsychoRandomMovement : MonoBehaviour
{
    public float wanderRadius = 10f; // NPC가 이동할 반경
    public float chaseDistance = 10f; // 플레이어를 추적하기 시작하는 거리
    private NavMeshAgent navMeshAgent;
    public Transform player; // 플레이어의 Transform
    public AudioSource footstepAudioSource; // 발걸음 소리 오디오 소스
    public float maxVolumeDistance = 3f; // 소리가 최대인 거리

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform; // "Player" 태그를 가진 객체를 찾아 플레이어로 설정
            }
            else
            {
                Debug.LogError("Player with tag 'Player' not found!");
            }
        }
        if (footstepAudioSource == null)
        {
            footstepAudioSource = GetComponent<AudioSource>(); // 오디오 소스 가져오기
        }

        // 발걸음 소리 속도 조절 (0.5배 늦추기)
        footstepAudioSource.pitch = 0.5f;
        footstepAudioSource.volume = 1.0f; // 볼륨 초기화
        footstepAudioSource.Play(); // 오디오 소스 재생

        Wander(); // 시작할 때 랜덤 이동 시작
    }

    void Update()
    {
        if (player == null) return;

        // y 값을 제외한 거리 계산
        Vector3 npcPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 playerPosition = new Vector3(player.position.x, 0, player.position.z);
        float distanceToPlayer = Vector3.Distance(npcPosition, playerPosition);

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

        // 발걸음 소리 크기 조절
        AdjustFootstepVolume(distanceToPlayer);
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

    // 발걸음 소리 크기 조절 함수
    void AdjustFootstepVolume(float distanceToPlayer)
    {
        // 거리 비율 계산 (0은 플레이어와 가장 가까움, 1은 maxVolumeDistance 이상 멀어짐)
        float distanceRatio = Mathf.Clamp01(distanceToPlayer / maxVolumeDistance);
        // 소리 크기 조절 (가까울수록 소리가 커짐)
        footstepAudioSource.volume = 1 - distanceRatio;
    }
}
