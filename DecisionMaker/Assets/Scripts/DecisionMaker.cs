using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System;
using System.Reflection;
using System.Linq;

public class DecisionMaker : MonoBehaviour
{
    public List<Scripts> data = new List<Scripts>();    // Data of all scripts in a list
    public List<string> classNames;                     // List of all behavior class names
    public List<Type> types;                            // List of all behavior types

    private NavMeshAgent myNavMeshAgent;                // NavMeshAgent
    private Rigidbody rigidbody;                        // Rigidbody of an agent

    private int highestScore = 0;                       // Current highest score
    private int index;                                  // Index of the highest scores script
    private object[] classObject;                       // Class of a behavior script
    private object[] scoreObject;                       // Score given from a behavior script
    private object behaviourObject;                     // Agent's behavior, which is going to be executed
    private object targetObject = null;                 // Target for an agent to look chase/look for
    private int[] scores;                               // All scores

    private void Start()
    {
        // Create a list of class names
        classNames = new List<string>();
        // Create a list of behavior types
        types = new List<Type>();

        // Get the components
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        // Assign all arrays length
        classObject = new object[data.Count];
        scoreObject = new object[data.Count];
        scores = new int[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            // Get all class names into a list
            classNames.Add(data[i].Script.name);
            print(classNames[i]);

            // Add the class type
            types.Add(Type.GetType(classNames[i]));

            // Invoke a constructor
            ConstructorInfo constructor = types[i].GetConstructor(new[] { typeof(NavMeshAgent), typeof(Rigidbody) });
            classObject[i] = constructor.Invoke(classObject[i], new object[] { myNavMeshAgent, rigidbody });
        }
    }

    public void Update()
    {
        for (int i = 0; i < data.Count; i++)
        {
            // Check if the scripts used for detection
            if(types[i].GetMethod("Detection") != null)
            {
                // Invoke it's method (function)
                MethodInfo target = types[i].GetMethod("Detection", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                targetObject = target.Invoke(classObject[i], new object[] { });
            }

            // Check if the scripts have score evaluators
            if (types[i].GetMethod("ScoreEvaluator") != null)
            {
                // Invoke it's method (function
                MethodInfo score = types[i].GetMethod("ScoreEvaluator", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                scoreObject[i] = score.Invoke(classObject[i], new object[] { (Transform)targetObject });
                // Add the score to array
                scores[i] = (int)scoreObject[i];
            }
        }
        // Get the highest score
        highestScore = scores.Max();
        // Get the highest score's array index
        index = Array.IndexOf(scores, highestScore);
        Debug.Log(highestScore);
        Debug.Log(targetObject);

        // Execute the behavior, that has the highest score
        MethodInfo behaviour = types[index].GetMethod("BehaviourExecute", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        behaviourObject = behaviour.Invoke(classObject[index], new object[] { (Transform)targetObject });
    }
}
