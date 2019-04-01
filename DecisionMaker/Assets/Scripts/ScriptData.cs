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
    public GameObject holder;
    public List<string> classNames;
    public List<Type> types;

    private NavMeshAgent myNavMeshAgent;
    private Rigidbody rigidbody;
    private int highestScore = 0;
    private int index;
    private MonoBehaviour[] myScripts;
    private object classObject;
    private object scoreObject;
    private object behaviourObject;

    private void Start()
    {
        // Create a list of class names
        classNames = new List<string>();
        types = new List<Type>();

        myNavMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponent<Rigidbody>();

        //classObject = new object[] { };
        //scoreObject = new object[] { };
        //behaviourObject = new object[] { };

        //for (int i=0; i<data.Count; i++)
        //{
        //    // Get all class names into a list
        //    classNames.Add(data[i].Script.name);
        //    print(classNames[i]);
        //
        //    // Get the class
        //    types.Add(Type.GetType(classNames[i]));
        //
        //    // Get the constructor and invoke it
        //    ConstructorInfo constructor = types[i].GetConstructor(Type.EmptyTypes);
        //    classObject = constructor.Invoke(this, new object[] { });
        //
        //}

        for (int i = 0; i < data.Count; i++)
        {
            // Get all class names into a list
            classNames.Add(data[i].Script.name);
            print(classNames[i]);

            // Get the class
            types.Add(Type.GetType(classNames[i]));

            //ConstructorInfo constructor = types[i].GetConstructor(new[] { typeof(NavMeshAgent), typeof(Rigidbody) });
            //classObject = constructor.Invoke(classObject, new object[] { myNavMeshAgent, rigidbody });
        }

        // Get the constructor and invoke it
        foreach (Type type in types)
        {
            ConstructorInfo constructor = type.GetConstructor(new[] { typeof(NavMeshAgent), typeof(Rigidbody) });
            classObject = constructor.Invoke(classObject, new object[] { myNavMeshAgent, rigidbody });
        }

    }

    public void Update()
    {
        //for (int i = 0; i < data.Count; i++)
        //{
        //    // Get the method and invoke it
        //    MethodInfo score = types[i].GetMethod("ScoreEvaluator");
        //    object test = score.Invoke(classObject, new object[] { });

        //    GetHighscore((int)test, i);

        //    Debug.Log(highestScore);
        //}
        //MethodInfo behaviour = types[index].GetMethod("BehaviourExecute");
        //object behaviourMethod = behaviour.Invoke(classObject, new object[] { });

        for (int i = 0; i < data.Count; i++)
        {
            // Get the method and invoke it
            MethodInfo score = types[i].GetMethod("ScoreEvaluator");
            scoreObject = score.Invoke(classObject, new object[] { });
            GetHighscore((int)scoreObject, i);

            Debug.Log(highestScore);
        }

        MethodInfo behaviour = types[index].GetMethod("BehaviourExecute", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        behaviourObject = behaviour.Invoke(classObject, new object[] { });
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
