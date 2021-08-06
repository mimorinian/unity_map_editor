using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[ExecuteInEditMode]
public class mxc_grid : mxc_object
{
    public Tilemap tilemap;

    private void Awake()
    {
        if (null == this.tilemap)
        {
            this.tilemap = this.GetComponentInChildren<Tilemap>();
        }
    }

    public override JObject Export()
    {
        var json_object = new JObject();
        json_object["name"] = name;
        json_object["type"] = "grid";
        var sorting_layer_id = tilemap.gameObject.GetComponent<TilemapRenderer>().sortingLayerID;
        json_object["sort"] = SortingLayer.GetLayerValueFromID(sorting_layer_id);
        json_object["atlas"] = atlas;
        json_object["cells"] = new JObject();
        json_object["debug"] = debug;
        json_object["area_type"] = (int)this.area_type;
        json_object["city_area_id"] = this.city_area_id;
        if (null != tilemap)
        {
            var TextStyle = new GUIStyle();
            TextStyle.normal.textColor = Color.red;
            TextStyle.fontSize = 20;
            Vector3 tilePosition;
            Vector3Int coordinate = new Vector3Int(0, 0, 0);

            // 导出laya对调x y轴
            for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
            {
                for (int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
                {
                    var laya_x = -j;
                    var laya_y = i;
                    if (null == json_object["cells"][laya_x.ToString()])
                    {
                        json_object["cells"][laya_x.ToString()] = new JObject();
                    }
                    coordinate.x = i;
                    coordinate.y = j;
                    tilePosition = tilemap.CellToWorld(coordinate);
                    var tilebase = tilemap.GetTile(coordinate);
                    TileData tiledata = new TileData();
                    try
                    {
                        tilebase.GetTileData(coordinate, null, ref tiledata);
                        if (tiledata.sprite != null)
                        {
                            var obj = new JArray();
                            obj.Add(this.mxc_texture_object.get_texture_id(tiledata.sprite.name));
                            var eulerAngles = tilemap.GetTransformMatrix(coordinate).rotation.eulerAngles;
                            obj.Add(Mathf.Approximately(eulerAngles.y, 180) ? 1 : 0);

                            var array = new JArray();
                            array.Add(obj);
                            json_object["cells"][laya_x.ToString()][laya_y.ToString()] = array;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        json_object["sprites"] = this.mxc_texture_object.get_textures_jobject();
        return json_object;
    }
}
