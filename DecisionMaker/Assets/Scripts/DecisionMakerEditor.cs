using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor( typeof( DecisionMaker ) )]
public class DecisionMakerEditor : Editor {
    // Have to play to choose enumerators

    //DecisionMaker DM;

    //public void OnEnable()
    //{
    //    DM = (DecisionMaker)target;
    //}

    //public override void OnInspectorGUI()
    //{
    //    DM.enumerator = EditorGUILayout.EnumPopup(DM.enumerator);
    //}
}
