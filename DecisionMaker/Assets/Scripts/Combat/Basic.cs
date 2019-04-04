using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Basic
{

    public NavMeshAgent myNavMeshAgent;
    public Rigidbody rigidbody;

	public Basic(NavMeshAgent agent, Rigidbody rb)
    {
        myNavMeshAgent = agent;
        rigidbody = rb;
    }

    public int ScoreEvaluator(Transform target)
    {
        if(target != null)
        {
            return 100;
        }
        else
        {
            return 0;
        }
    }

    public void BehaviourExecute(Transform target)
    {
        Vector3 targetPosition = new Vector3(target.position.x, myNavMeshAgent.transform.position.y, target.position.z);
        myNavMeshAgent.transform.LookAt(targetPosition);
        myNavMeshAgent.SetDestination(target.position);
    }
}
