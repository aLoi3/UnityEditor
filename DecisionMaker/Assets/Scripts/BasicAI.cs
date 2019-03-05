using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[Serializable]
public struct Scripts
{
    public enum ScriptType
    {
        Navigation,
        Something
    }
    
    public ScriptType Type;
    public MonoScript Script;
    //public int Count;

}
