using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[ExecuteInEditMode]
public class mxc_cell : mxc_object
{
    public override JObject Export()
    {
        var json_object = new JObject();
        json_object["name"] = name;
        json_object["type"] = "cell";
        json_object["sort"] = SortingLayer.GetLayerValueFromName(sortingLayer);
        json_object["atlas"] = atlas;
        json_object["cells"] = new JObject();
        json_object["debug"] = debug;
        json_object["area_type"] = (int)this.area_type;
        json_object["city_area_id"] = this.city_area_id;
        json_object["enable_collider"] = this.enable_collider;
        var grid = this.transform.parent.gameObject.GetComponentInChildren<Grid>();

        // 导出laya对调x y轴
        var renderers = this.GetComponentsInChildren<SpriteRenderer>();
        if (null != renderers && 0 < renderers.Length)
        {
            foreach (var renderer in renderers)
            {
                if (null == renderer.sprite || !renderer.gameObject.activeInHierarchy)
                {
                    continue;
                }

                var cell_coordinate = grid.WorldToCell(renderer.transform.position);
                var laya_x = -cell_coordinate.y;
                var laya_y = cell_coordinate.x;

                if (null == json_object["cells"][laya_x.ToString()])
                {
                    json_object["cells"][laya_x.ToString()] = new JObject();
                }

                if (null == json_object["cells"][laya_x.ToString()][laya_y.ToString()])
                {
                    json_object["cells"][laya_x.ToString()][laya_y.ToString()] = new JArray();
                }

                var obj = new JArray();
                obj.Add(this.mxc_texture_object.get_texture_id(renderer.sprite.name));
                obj.Add(renderer.flipX ? 1 : 0);
                var width = renderer.sprite.texture.width * renderer.transform.lossyScale.x / 256f;
                var height = renderer.sprite.texture.height * renderer.transform.lossyScale.y / 256f;
                obj.Add(renderer.transform.position.x);
                obj.Add(renderer.transform.position.y);
                obj.Add(width);
                obj.Add(height);
                var array = json_object["cells"][laya_x.ToString()][laya_y.ToString()] as JArray;
                array.Add(obj);

                var temp_array = new List<JToken>();
                foreach (var item in array)
                {
                    temp_array.Add(item as JToken);
                }

                array.Clear();
                for (var i = 0; i < temp_array.Count; ++i)
                {
                    array.Add(temp_array);
                }
            }
        }

        json_object["sprites"] = this.mxc_texture_object.get_textures_jobject();
        return json_object;
    }

    public override void Update()
    {
        base.Update();
        int sorting_order = 0;
        this.change_sorting_order(this.transform, ref sorting_order);
    }

    void change_sorting_order(Transform child, ref int sorting_order)
    {
        child.position = new Vector3(child.position.x, child.position.y, 0);

        var sprite_render = child.gameObject.GetComponent<Renderer>();
        if (null != sprite_render)
        {
            var x = child.transform.position.x + sprite_render.bounds.size.x / 2;
            var y = child.transform.position.y;
            var child_sorting_order = Mathf.FloorToInt(x * 100 - y * 100) + 10000;
            sprite_render.sortingOrder = child_sorting_order;
        }

        var children = new List<Transform>();

        for (var i = 0; i < child.childCount; ++i)
        {
            children.Add(child.GetChild(i));
        }

        children.Sort((a, b)=>
        {
            return a.GetSiblingIndex().CompareTo(b.GetSiblingIndex());
        });

        for (var i = 0; i < children.Count; ++i)
        {
            change_sorting_order(children[i], ref sorting_order);
        }
    }
}
