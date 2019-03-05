using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
}
