using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class psychopathNavMesh : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform pointGroup;
    private Transform[] points;
    public int currentIndex;

    public enum AGENT_STAGE
    {
        STAGE1, STAGE2, STAGE3
    }

    public AGENT_STAGE e_AGENT_STAGE;

    void Start()
    {
        currentIndex = 2;
        points = pointGroup.GetComponentsInChildren<Transform>();
        Debug.Log(points.Length);
        agent.SetDestination(points[currentIndex].position);
    }

    void Update()
    {
        Debug.Log("agent.remainingDistance : " + agent.remainingDistance);

        if (agent.remainingDistance <= 1f)
        {
            currentIndex++;
            switch (e_AGENT_STAGE)
            {
                case AGENT_STAGE.STAGE1:
                    if (currentIndex >= 3)
                        currentIndex = 0;
                    break;
                case AGENT_STAGE.STAGE2:
                    if (currentIndex >= 6)
                        currentIndex = 0;
                    break;
                case AGENT_STAGE.STAGE3:
                    if (currentIndex >= 10)
                        currentIndex = 0;
                    break;
            }

            Debug.Log(points[currentIndex].name);
        agent.SetDestination(points[currentIndex].position);
        }
    }

        public void OnSetStage(AGENT_STAGE stage)
        {
            e_AGENT_STAGE = stage;
        }
    }
