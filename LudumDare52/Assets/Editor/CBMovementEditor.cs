using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CBMovement))]
public class CBMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CBMovement cbMovement = (CBMovement)target;

        if (GUILayout.Button("Speed 5"))
        {
            cbMovement.OnUpdateSpeed(5);
        }
        if (GUILayout.Button("Speed 2"))
        {
            cbMovement.OnUpdateSpeed(2);
        }
        if (GUILayout.Button("Pause Motion"))
        {
            cbMovement.PauseMotion();
        }
        if (GUILayout.Button("Resume Motion"))
        {
            cbMovement.EnableMotion();
        }
    }
}
