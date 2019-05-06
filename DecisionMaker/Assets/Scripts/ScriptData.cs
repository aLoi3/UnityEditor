using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[Serializable]
public struct Scripts
{
    // Types of scripts
    public enum ScriptType
    {
        Navigation,
        Detection,
        Combat
    }
    
    public ScriptType Type;
    public MonoScript Script;
}
