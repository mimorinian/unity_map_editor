using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(mxc_obstacle))]
public class mxc_obstacle_editor : mxc_object_editor
{
    mxc_obstacle obstacle_obj;
    protected SerializedProperty tilemap;

    protected override void OnEnable()
    {
        base.OnEnable();
        obstacle_obj = target as mxc_obstacle;
        tilemap = serializedObject.FindProperty("tilemap");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(area_type, new GUIContent("area_type"), GUILayout.Height(20));
        if (area_type.enumValueIndex == (int)mxc_area_type.主城区域)
        {
            EditorGUILayout.PropertyField(city_area_id, new GUIContent("city_area_id"), GUILayout.Height(20));
        }
        EditorGUILayout.PropertyField(tilemap, new GUIContent("tilemap"), GUILayout.Height(20));
        serializedObject.ApplyModifiedProperties();
    }
}
