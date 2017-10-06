using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentScale : MonoBehaviour {

    // The game object that materialises where the player is pointing.
    [Tooltip("The game object that materialises where the player is pointing.")]
    public Vector3 ScalePercent = new Vector3(100,100,100);
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = ScalePercent / 100;
    }
}
