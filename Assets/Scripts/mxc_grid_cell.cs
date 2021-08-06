using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[ExecuteInEditMode]
public class mxc_grid_cell : mxc_object
{
    private void Awake()
    {
    }

    public override JObject Export()
    {
        var json_object = new JObject();
        json_object["name"] = name;
        json_object["type"] = "grid_cell";
        json_object["sort"] = SortingLayer.GetLayerValueFromName(sortingLayer);
        json_object["atlas"] = atlas;
        json_object["cells"] = new JObject();
        json_object["sprites"] = this.mxc_texture_object.get_textures_jobject();
        json_object["debug"] = debug;
        json_object["area_type"] = (int)this.area_type;
        json_object["city_area_id"] = this.city_area_id;
        return json_object;
    }
}
