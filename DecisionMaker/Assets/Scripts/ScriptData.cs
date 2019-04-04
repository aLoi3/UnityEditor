using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System;
using System.Reflection;
using System.Linq;

public class ScriptData : MonoBehaviour
{
    public List<Scripts> data = new List<Scripts>();
    public List<string> classNames;
    public List<Type> types;

    private NavMeshAgent myNavMeshAgent;
    private Rigidbody rigidbody;
    private int highestScore = 0;
    private int index;
    private object[] classObject;
    private object[] scoreObject;
    private object behaviourObject;
    private object targetObject = null;
    private object[] parameters;
    private int[] scores;
    private ConstructorInfo constructor;

    private void Start()
    {
        // Create a list of class names
        classNames = new List<string>();
        types = new List<Type>();

        myNavMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        object[] parameters = new object[] { myNavMeshAgent, rigidbody };

        classObject = new object[data.Count];
        scoreObject = new object[data.Count];
        scores = new int[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            // Get all class names into a list
            classNames.Add(data[i].Script.name);
            print(classNames[i]);

            // Get the class
            types.Add(Type.GetType(classNames[i]));

            ConstructorInfo constructor = types[i].GetConstructor(new[] { typeof(NavMeshAgent), typeof(Rigidbody) });
            classObject[i] = constructor.Invoke(classObject[i], new object[] { myNavMeshAgent, rigidbody });
        }
    }

    public void Update()
    {
        for (int i = 0; i < data.Count; i++)
        {
            if(types[i].GetMethod("Detection") != null)
            {
                MethodInfo target = types[i].GetMethod("Detection", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                targetObject = target.Invoke(classObject[i], new object[] { });
            }

            // Get the method and invoke it
            if (types[i].GetMethod("ScoreEvaluator") != null)
            {
                MethodInfo score = types[i].GetMethod("ScoreEvaluator", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                scoreObject[i] = score.Invoke(classObject[i], new object[] { (Transform)targetObject });
                scores[i] = (int)scoreObject[i];
            }
        }
        highestScore = scores.Max();
        index = Array.IndexOf(scores, highestScore);
        Debug.Log(highestScore);
        Debug.Log(targetObject);

        MethodInfo behaviour = types[index].GetMethod("BehaviourExecute", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        behaviourObject = behaviour.Invoke(classObject[index], new object[] { (Transform)targetObject });
    }
}
