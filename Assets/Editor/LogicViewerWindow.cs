using Packages.Rider.Editor.Util;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LogicViewerWindow : EditorWindow
{
    [MenuItem("Logic/Logic Viewer")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow<LogicViewerWindow>("Logic Viewer");
    }

    private string indexText = "0";
    private string valueText = "0";

    private float[] logicArray;
    private bool wasPlayable;
    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        GUILayout.Label("Logic Index");
        indexText = GUILayout.TextField(indexText);
        GUILayout.Space(20);
        GUILayout.Label("New Value");
        valueText = GUILayout.TextField(valueText);
        if (Application.isPlaying)
        {

            if (!wasPlayable)
                OnStart();

            int index = 0;
            int value = 0;
            

            if(indexText != "" || valueText != "") { 
            index = int.Parse(indexText);
            value = int.Parse(valueText);
            }



            GUILayout.Space(20);

            GUILayout.Label("Current Value");
            GUILayout.Label(logicArray[index].ToString());


            GUILayout.Space(20);

            if (GUILayout.Button("Update"))
            {

                GameSystem.Instance.LogicUpdate(value, index, GameSystem.operation._SET);
            }
        }
        else if (wasPlayable)
            OnStop();


        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        wasPlayable = Application.isPlaying;
    }
    private void OnStart()
    {
        GameSystem.Instance.onLogicUpdate += HandleLogicUpdate;
        logicArray = new float[100];
    }

    private void OnStop()
    {
        GameSystem.Instance.onLogicUpdate -= HandleLogicUpdate;
    }

    private void HandleLogicUpdate(float[] logicArray)
    {
        this.logicArray = logicArray;
    }


}
