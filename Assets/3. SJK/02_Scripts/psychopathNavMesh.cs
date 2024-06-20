using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PsychoPathNPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] points123;
    public Transform[] points123456;
    public Transform[] points12345678910;

    private int destPoint = 0;
    private bool firstDoorOpened = false;
    private bool secondDoorOpened = false;

    private Transform[] currentPoints;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPoints = points123;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (currentPoints.Length == 0)
            return;

        agent.destination = currentPoints[destPoint].position;
        destPoint = (destPoint + 1) % currentPoints.Length;
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    public void OnFirstDoorOpened()
    {
        if (!firstDoorOpened)
        {
            firstDoorOpened = true;
            currentPoints = points123456;
            destPoint = 0;
        }
    }

    public void OnSecondDoorOpened()
    {
        if (!secondDoorOpened)
        {
            secondDoorOpened = true;
            currentPoints = points12345678910;
            destPoint = 0;
        }
    }
}
