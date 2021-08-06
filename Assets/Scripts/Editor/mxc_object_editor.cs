using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;

[CustomEditor(typeof(mxc_object))]
public class mxc_object_editor : Editor
{
    protected string[] sortingLayerNames = null;
    protected SerializedProperty atlas;
    protected SerializedProperty area_type;
    protected SerializedProperty city_area_id;
    protected SerializedProperty debug;
    protected SerializedProperty enable_collider;
    protected mxc_object obj;

    protected virtual void OnEnable()
    {
        System.Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        this.sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);
        obj = target as mxc_object;

        atlas = serializedObject.FindProperty("atlas");
        area_type = serializedObject.FindProperty("area_type");
        city_area_id = serializedObject.FindProperty("city_area_id");
        debug = serializedObject.FindProperty("debug");
        enable_collider = serializedObject.FindProperty("enable_collider");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(atlas, new GUIContent("atlas"), GUILayout.Height(20));

        int index = SortingLayer.GetLayerValueFromName(obj.sortingLayer);
        index = EditorGUILayout.Popup("sortingLayer", index, this.sortingLayerNames, GUILayout.ExpandWidth(true));
        obj.change_sorting_layer(this.sortingLayerNames[index]);

        EditorGUILayout.PropertyField(area_type, new GUIContent("area_type"), GUILayout.Height(20));
        if (area_type.enumValueIndex == (int)mxc_area_type.主城区域)
        {
            EditorGUILayout.PropertyField(city_area_id, new GUIContent("city_area_id"), GUILayout.Height(20));
        }
        EditorGUILayout.PropertyField(debug, new GUIContent("debug"), GUILayout.Height(20));
        EditorGUILayout.PropertyField(enable_collider, new GUIContent("enable_collider"), GUILayout.Height(20));
        serializedObject.ApplyModifiedProperties();
    }
}
