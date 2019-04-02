using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System;
using System.Reflection;

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
    private object scoreObject;
    private object behaviourObject;
    private object[] parameters;
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
            // Get the method and invoke it
            MethodInfo score = types[i].GetMethod("ScoreEvaluator", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            scoreObject = score.Invoke(classObject[i], new object[] { });
            GetHighscore((int)scoreObject, i);

            Debug.Log(highestScore);
        }

        MethodInfo behaviour = types[index].GetMethod("BehaviourExecute", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        behaviourObject = behaviour.Invoke(classObject[index], new object[] { });
    }

    public void GetHighscore(int score, int i)
    {
        if(score < highestScore)
        {
            return;
        }
        else
        {
            highestScore = score;
            index = i;
        }
    }
}
