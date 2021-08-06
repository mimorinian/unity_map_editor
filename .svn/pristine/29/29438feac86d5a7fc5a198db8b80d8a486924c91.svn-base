using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(mxc_grid))]
public class mxc_grid_editor : mxc_object_editor
{
    mxc_grid grid_obj;
    protected SerializedProperty tilemap;

    protected override void OnEnable()
    {
        base.OnEnable();
        grid_obj = target as mxc_grid;
        tilemap = serializedObject.FindProperty("tilemap");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(tilemap, new GUIContent("tilemap"), GUILayout.Height(20));
        serializedObject.ApplyModifiedProperties();
    }
}
