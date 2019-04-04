﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Some1
{
    // Detection Radius

    private int test;
    public Vector3 wanderTarget;
    public float wanderRange = 4.0f;
    public NavMeshHit navHit;
    public float delta = 0.0f;
    public NavMeshAgent myNavMeshAgent;
    public Rigidbody rigidbody;
    public float detectionRange = 10;

    public Some1(NavMeshAgent agent, Rigidbody rb)
    {
        test = 10;
        myNavMeshAgent = agent;
        rigidbody = rb;
    }

    public void PrintStr()
    {
        Debug.Log(test);
    }

    public Transform Detection()
    {
        Transform target;
        GameObject player = GameObject.FindWithTag("Player");
        
        if((player.transform.position - myNavMeshAgent.transform.position).magnitude <= detectionRange)
        {
            target = player.transform;
        }
        else
        {
            target = null;
        }

        return target;
    }

    //public int ScoreEvaluator()
    //{
    //    return 90;
    //}

    //public void BehaviourExecute()
    //{
    //    Debug.Log("I'm navigating... + Some1");
    //    delta += Time.deltaTime;

    //    // Move to another point every 3 seconds
    //    if (delta >= 3.0f)
    //    {
    //        /* Next if statements are checks if the enemy has reached the assigned position */
    //        // Checks if character is going to a position
    //        if (!myNavMeshAgent.pathPending)
    //        {
    //            // Checks how far or close is the position
    //            if (myNavMeshAgent.remainingDistance <= myNavMeshAgent.stoppingDistance)
    //            {
    //                // Checks whether it has path and the movement velocity to after assign a new position
    //                if (!myNavMeshAgent.hasPath || myNavMeshAgent.velocity.sqrMagnitude == 0.0f)
    //                {
    //                    // Find a random point to go to
    //                    if (RandomWanderTarget(rigidbody.position, wanderRange, out wanderTarget))
    //                    {
    //                        // Debug drawing
    //                        Debug.DrawRay(wanderTarget, Vector3.up, Color.blue, 1.0f);

    //                        // Move to the assigned position
    //                        myNavMeshAgent.SetDestination(wanderTarget);

    //                        delta = 0.0f;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    //{
    //    // Sets invisible sphere for enemy to decide where to go
    //    Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
    //    if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
    //    {
    //        // Sends out position for enemy to go to
    //        result = navHit.position;
    //        return true;
    //    }
    //    else
    //    {
    //        result = centre;
    //        return false;
    //    }
    //}
}