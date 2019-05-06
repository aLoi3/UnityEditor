using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander
{
    public Vector3 wanderTarget;
    public NavMeshHit navHit;
    public NavMeshAgent myNavMeshAgent;
    public Rigidbody rigidbody;

    public float wanderRange = 4.0f;
    public float delta = 0.0f;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="agent"> NavMeshAgent component </param>
    /// <param name="rb"> Rigidbody component </param>

    public Wander(NavMeshAgent agent, Rigidbody rb)
    {
        myNavMeshAgent = agent;
        rigidbody = rb;
    }

    /// <summary>
    /// Evaluates the score.
    /// Right now is constant, will be dynamic in later development
    /// </summary>
    /// <param name="target"> Player's Transform </param>
    /// <returns> Integer - score </returns>

    public int ScoreEvaluator(Transform target)
    {
        if(target == null)
        {
            return 100;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Wander behavior - chooses a random point around it and goes there
    /// </summary>
    /// <param name="target"> Player's transform (not needed, will be removed in lated development) </param>

    public void BehaviourExecute(Transform target)
    {
        Debug.Log("I'm navigating... + Nav1");
        delta += Time.deltaTime;

        // Move to another point every 3 seconds
        if (delta >= 3.0f)
        {
            /* Next if statements are checks if the enemy has reached the assigned position */
            // Checks if character is going to a position
            if (!myNavMeshAgent.pathPending)
            {
                // Checks how far or close is the position
                if (myNavMeshAgent.remainingDistance <= myNavMeshAgent.stoppingDistance)
                {
                    // Checks whether it has path and the movement velocity to after assign a new position
                    if (!myNavMeshAgent.hasPath || myNavMeshAgent.velocity.sqrMagnitude == 0.0f)
                    {
                        // Find a random point to go to
                        if (RandomWanderTarget(rigidbody.position, wanderRange, out wanderTarget))
                        {
                            // Debug drawing
                            Debug.DrawRay(wanderTarget, Vector3.up, Color.blue, 1.0f);

                            // Move to the assigned position
                            myNavMeshAgent.SetDestination(wanderTarget);

                            delta = 0.0f;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Chooses a random position for the agent to go to
    /// </summary>
    /// <param name="centre"> Center of the agent </param>
    /// <param name="range"> Range around the agent to look a position for </param>
    /// <param name="result"> End position </param>
    /// <returns> True - if found a position, False - if hasn't </returns>
    
    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        // Sets invisible sphere for enemy to decide where to go
        Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
        if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {
            // Sends out position for enemy to go to
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre;
            return false;
        }
    }
}
