using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    Vector3 actualDestination;

    private void Start()
    {
        agent = GetComponent <NavMeshAgent>();
        animator = GetComponent <Animator>();
        actualDestination = transform.position;
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (CheckDistanceFrom(actualDestination) <= agent.stoppingDistance)
        {
            agent.SetDestination(transform.position);
        }
    }

    public void Move(EventEnvironmentArgs args)
    {
        agent.SetDestination(args.destination);
        actualDestination = args.destination;
    }

    public float CheckDistanceFrom(Vector3 chico)
    {
        return Vector3.Distance(transform.position, chico);
    }

}
