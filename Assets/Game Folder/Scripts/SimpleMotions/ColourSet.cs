using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSet : MonoBehaviour {

    // Renderer
    private Renderer r;

    public void ChangeColour(Color newCol)
    {
        if (r != null)
            r.material.SetColor("_Color", newCol);
        else
            r = this.GetComponent<Renderer>();
    }
}