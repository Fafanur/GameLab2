using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MazeGenerater))]
public class MazeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MazeGenerater myScript = (MazeGenerater) target;
        if (GUILayout.Button("Build Maze"))
        {
            myScript.GenerateLabirynth();
        }
    }
}
