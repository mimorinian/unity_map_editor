using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class mxc_object : MonoBehaviour
{
    public string atlas;
    public string sortingLayer;
    public Vector3 grid_size;
    public mxc_area_type area_type = mxc_area_type.默认区域;
    public int city_area_id = 0;
    public bool debug = false;
    public bool enable_collider = false;
    protected mxc_texture mxc_texture_object = new mxc_texture();

    public virtual JObject Export()
    {
        return null;
    }

    public virtual void Update()
    {
        this.change_sorting_layer(this.sortingLayer);
    }

    public void change_sorting_layer(string sortingLayer)
    {
        this.sortingLayer = sortingLayer;
        var renders = this.GetComponentsInChildren<Renderer>();
        foreach (var render in renders)
        {
            render.sortingLayerID = SortingLayer.NameToID(sortingLayer);
        }
    }
}
