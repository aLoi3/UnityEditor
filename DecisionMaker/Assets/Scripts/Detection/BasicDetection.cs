using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicDetection
{
    public NavMeshAgent myNavMeshAgent;     // Agent's NavMeshAgent
    public Rigidbody rigidbody;             // Agent's rigidbody
    public float detectionRange = 10;       // Radius at which the agent should detect the player

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="agent"> NavMeshAgent component </param>
    /// <param name="rb"> Rigidbody component </param>

    public BasicDetection(NavMeshAgent agent, Rigidbody rb)
    {
        myNavMeshAgent = agent;
        rigidbody = rb;
    }

    /// <summary>
    /// Detection script
    /// </summary>
    /// <returns> Player's transform </returns>

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
}
