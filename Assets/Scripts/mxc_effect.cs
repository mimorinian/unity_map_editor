using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class mxc_effect : MonoBehaviour
{
    private void Update()
    {
        var effects = this.GetComponentsInChildren<Renderer>();
        foreach (var effect in effects)
        {
            effect.sortingLayerName = "effect";
        }
    }
}
