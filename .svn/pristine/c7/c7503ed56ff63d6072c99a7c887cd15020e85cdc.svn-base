using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;

[CustomEditor(typeof(mxc_map))]
public class mxc_map_editor : Editor
{
    protected string[] sortingLayerNames = null;
    mxc_map obj;

    void OnEnable()
    {
        obj = target as mxc_map;
        System.Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        this.sortingLayerNames = (string[])sortingLayersProperty.GetValue(null, new object[0]);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (obj.enableSortingLayer)
        {
            int index = SortingLayer.GetLayerValueFromName(obj.sortingLayer);
            index = EditorGUILayout.Popup("sortingLayer", index, this.sortingLayerNames, GUILayout.ExpandWidth(true));
            obj.change_sorting_layer(this.sortingLayerNames[index]);
        }

        if (GUILayout.Button("Export"))
        {
            obj.Export();
        }

        if (GUILayout.Button("ExportSortingLayer"))
        {
            obj.ExportSortingLayer();
        }
    }
}
