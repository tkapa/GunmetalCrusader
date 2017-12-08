using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foliage : MonoBehaviour {

    public float swayStrength = 2.0f;
    public float swaySpeed = 0.5f;

	// Use this for initialization
	void Start () {
        Shader.SetGlobalFloat("_SwayStrength", swayStrength);
        Shader.SetGlobalFloat("_SwaySpeed", swaySpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
