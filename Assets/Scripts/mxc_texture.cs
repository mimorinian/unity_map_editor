using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class mxc_texture
{
    protected List<string> textures = new List<string>();

    public int get_texture_id(string TextureName)
    {
        TextureName += ".png";
        int index = this.textures.Count;
        for (var i = 0; i < this.textures.Count; ++i)
        {
            var texture = this.textures[i];
            if (texture == TextureName)
            {
                index = i;
                break;
            }
        }

        if (index == this.textures.Count)
        {
            this.textures.Add(TextureName);
        }

        return index;
    }

    public JArray get_textures_jobject()
    {
        var obj = new JArray();
        for (var i = 0; i < this.textures.Count; ++i)
        {
            var texture = this.textures[i];
            obj.Add(texture);
        }
        return obj;
    }
}
