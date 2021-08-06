using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[ExecuteInEditMode]
public class mxc_map : MonoBehaviour
{
    public string export_path;
    public string export_sorting_layer_path;
    public Vector3 grid_size;
    public Grid.CellLayout grid_layout;
    public bool enableSortingLayer = false;
    [HideInInspector]
    public string sortingLayer;

    public void Export()
    {
        var export_obj = new JObject();
        export_obj["objects"] = new JObject();
        var objects = this.GetComponentsInChildren<mxc_object>();
        foreach (var obj in objects)
        {
            export_obj["objects"][obj.name] = obj.Export();
        }
        System.IO.File.WriteAllText(export_path, export_obj.ToString());
    }

    public void ExportSortingLayer()
    {
        var export_obj = new JObject();
        export_obj["objects"] = new JObject();
        var objects = this.GetComponentsInChildren<mxc_object>();
        foreach (var layer in SortingLayer.layers)
        {
            export_obj["objects"][layer.name] = layer.value;
        }
        System.IO.File.WriteAllText(export_sorting_layer_path, export_obj.ToString());
    }

    public void change_sorting_layer(string sortingLayer)
    {
        if (enableSortingLayer)
        {
            this.sortingLayer = sortingLayer;
            var objs = this.GetComponentsInChildren<mxc_object>();
            foreach (var obj in objs)
            {
                obj.change_sorting_layer(sortingLayer);
            }
        }
    }

    private void Update()
    {
        var grids = this.gameObject.GetComponentsInChildren<Grid>();
        foreach (var grid in grids)
        {
            grid.cellSize = grid_size;
            grid.cellLayout = grid_layout;
        }

        var objects = this.gameObject.GetComponentsInChildren<mxc_object>();
        foreach (var obj in objects)
        {
            obj.transform.position = Vector3.zero;
            obj.grid_size = grid_size;
        }
    }
}
