using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Basic
{
    public NavMeshAgent myNavMeshAgent;     // Agent's NavMeshAgent component
    public Rigidbody rigidbody;             // Agent's rigidbody component

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="agent"> NavMeshAgent component </param>
    /// <param name="rb"> Rigidbody component </param>
  
	public Basic(NavMeshAgent agent, Rigidbody rb)
    {
        myNavMeshAgent = agent;
        rigidbody = rb;
    }

    /// <summary>
    /// Evaluates the score.
    /// Right now is constant, will be dynamic in later development
    /// </summary>
    /// <param name="target"> Player's transform </param>
    /// <returns> Integer - score </returns>
   
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

    /// <summary>
    /// Basic behavior - chase the player
    /// </summary>
    /// <param name="target"> Player's Transform </param>
    
    public void BehaviourExecute(Transform target)
    {
        Vector3 targetPosition = new Vector3(target.position.x, myNavMeshAgent.transform.position.y, target.position.z);
        myNavMeshAgent.transform.LookAt(targetPosition);
        myNavMeshAgent.SetDestination(target.position);
    }
}
