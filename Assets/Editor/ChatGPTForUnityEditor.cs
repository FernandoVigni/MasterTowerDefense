using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
[CustomEditor(typeof(ChatGPTAPIManager))]
public class ChatGPTForUnityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChatGPTAPIManager script = (ChatGPTAPIManager)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Ask"))
        {
            script.SendRequest();
        }

        GUILayout.Space(15);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Save Script"))
        {
            script.SaveScript();
        }

        if (GUILayout.Button("Clear")) 
        {
            script.Clear();
        }

        GUILayout.EndHorizontal();
    }
}
*/