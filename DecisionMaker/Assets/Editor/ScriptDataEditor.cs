using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

[CustomEditor (typeof ( ScriptData ) )]
public class ScriptDataEditor : Editor
{
    private ReorderableList list;


    private struct ScriptCreationParams
    {
        public Scripts.ScriptType Type;
        public string Path;
    }

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, 
            serializedObject.FindProperty("data"), 
            true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Script Data");
        };

        list.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                //EditorGUI.PropertyField(
                    //new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                    //element.FindPropertyRelative("Type"), GUIContent.none);
                EditorGUI.PropertyField(
                    new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("Script"), GUIContent.none);
                //EditorGUI.PropertyField(
                //    new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
                //    element.FindPropertyRelative("Count"), GUIContent.none);
            };

        // Highlight the asset when dragging
        list.onSelectCallback = (ReorderableList l) =>
        {
            var script = l.serializedProperty.GetArrayElementAtIndex(l.index).FindPropertyRelative("Script").objectReferenceValue as MonoScript;
            if (script)
            {
                EditorGUIUtility.PingObject(script);
            }
        }; 

        // Disables "Remove" button when there's only 1 element left
        /* list.onCanRemoveCallback = (ReorderableList l) =>
        {
            return l.count > 1;
        }; */

        // Gives a warning when a scripts is about to get removed
        //list.onRemoveCallback = (ReorderableList l) =>
        //{
        //    if (EditorUtility.DisplayDialog("Warning!",
        //        "Are you sure you want to delete the script?", "Yes", "No"))
        //    {
        //        ReorderableList.defaultBehaviours.DoRemoveButton(l);
        //    }
        //}; 

        // Adds a specific prefab with specific count when adding a new element
        list.onAddCallback = (ReorderableList l) =>
        {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            //element.FindPropertyRelative("Type").enumValueIndex = 0;
            //element.FindPropertyRelative("Count").intValue = 20;
            element.FindPropertyRelative("Script").objectReferenceValue = 
                AssetDatabase.LoadAssetAtPath("Assets/Scripts/Navigation/Nav1.cs",
                typeof(MonoScript)) as MonoScript;
        }; 

        list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) =>
        {
            var menu = new GenericMenu();
            var guids = AssetDatabase.FindAssets("", new[] { "Assets/Scripts/Navigation" });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Navigation/" + Path.GetFileNameWithoutExtension(path)),
                    false, clickHandler,
                    new ScriptCreationParams() { Type = Scripts.ScriptType.Navigation, Path = path });
            }
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Scripts/Something" });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Something/" + Path.GetFileNameWithoutExtension(path)),
                    false, clickHandler,
                    new ScriptCreationParams() { Type = Scripts.ScriptType.Something, Path = path });
            }
            menu.ShowAsContext();
        };
    }

    // Initilises custom GUI
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    // Handles dropdown menu that adds mobs (for now) 
    private void clickHandler(object target)
    {
        var data = (ScriptCreationParams)target;
        var index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        //element.FindPropertyRelative("Type").enumValueIndex = (int)data.Type;
        //element.FindPropertyRelative("Count").intValue =
        //    data.Type == Scripts.ScriptType.Something ? 1 : 20; // Boss count will be 1, Mob count 20
        element.FindPropertyRelative("Script").objectReferenceValue =
            AssetDatabase.LoadAssetAtPath(data.Path, typeof(MonoScript)) as MonoScript;
        serializedObject.ApplyModifiedProperties();
    }
}
