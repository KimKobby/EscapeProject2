using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PsychopathNPC : MonoBehaviour
{
    public float detectionDistance = 3f; // NPC가 캐릭터를 추적할 최대 거리
    public float detectionAngle = 90f;   // NPC가 캐릭터를 추적할 시야 각도
    private float randomMoveRadius = 10f; // NPC가 초기에 설정할 랜덤 이동 반경
    private float changeRadiusInterval = 1.5f; // 반경 변경 간격

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
            Debug.LogError("Player 태그를 가진 GameObject를 찾을 수 없습니다.");
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

        // 캐릭터와의 거리와 각도 계산
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // 캐릭터가 시야 내에 있고 추적 가능 거리 내에 있을 경우 추적
        if (distanceToPlayer <= detectionDistance && angleToPlayer <= detectionAngle)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            // 추적 불가능할 경우 랜덤 이동
            if (!agent.hasPath || agent.remainingDistance < 0.1f)
            {
                SetRandomDestination();
            }
        }

        // 일정 간격으로 랜덤 이동 반경 변경
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
            Debug.Log("NPC가 캐릭터에게 충돌하여 게임 종료!");
            isGameOver = true;
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    // NPC가 이동할 랜덤 지점을 계산하고 NavMeshAgent에 설정하는 함수
    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomMoveRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, randomMoveRadius, 1);

        agent.SetDestination(hit.position);
    }

    // 랜덤 이동 반경을 변경하는 함수
    void ChangeWanderRadius()
    {
        randomMoveRadius = Random.Range(5f, 15f); // 반경을 5에서 15 사이의 랜덤 값으로 변경
        SetRandomDestination(); // 새로운 반경으로 이동 설정
    }
}
