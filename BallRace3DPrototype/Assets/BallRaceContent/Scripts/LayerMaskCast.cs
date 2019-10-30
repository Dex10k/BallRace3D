using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskCast 
{
    // Start is called before the first frame update
static bool LayermaskCollisionCast(LayerMask p_layerMask, GameObject p_Object)
    {
        if(p_layerMask == (p_layerMask | (1 << p_Object.layer)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
