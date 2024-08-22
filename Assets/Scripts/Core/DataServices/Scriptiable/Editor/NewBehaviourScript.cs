using UnityEngine;
using UnityEditor;
using Core.DataServices.Scriptiable;

[CustomEditor(typeof(DataContainer))]

public class MyScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DataContainer myScriptableObject = (DataContainer)target;

        if (GUILayout.Button("Save"))
        {
            myScriptableObject.Save();
        }

        if (GUILayout.Button("Reset"))
        {
            myScriptableObject.ResetData();
        }
    }
}
