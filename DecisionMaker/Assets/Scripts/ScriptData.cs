using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

public class ScriptData : MonoBehaviour
{
    public List<Scripts> data = new List<Scripts>();
    public GameObject holder;
    
    private MonoBehaviour[] myScripts;

    private void Start()
    {
        for (int i = 0; i < data.Count; i++)
        {
            print(data[i].Script.name);
        }


    }

    private void Update()
    {
        Type t = data[0].Script.GetType();
        //t.GetMethod("PrintStr").Invoke(new object[] { });
    }
}
